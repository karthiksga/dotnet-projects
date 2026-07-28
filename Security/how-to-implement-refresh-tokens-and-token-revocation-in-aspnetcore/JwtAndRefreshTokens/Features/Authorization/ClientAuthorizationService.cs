using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAndRefreshTokens.Configuration;
using JwtAndRefreshTokens.Database;
using JwtAndRefreshTokens.Database.Entities;
using JwtAndRefreshTokens.Features.Authorization.Models;
using JwtAndRefreshTokens.Features.Shared;
using JwtAndRefreshTokens.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JwtAndRefreshTokens.Features.Authorization;

public class ClientAuthorizationService : IClientAuthorizationService
{
    private const string ErrorInvalidCredentials = "invalid_credentials";
    private const string ErrorUserNotFound = "user_not_found";
    private const string ErrorInvalidToken = "invalid_token";
    private const string ErrorInvalidInput = "invalid_input";

    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IOptions<AuthConfiguration> _authOptions;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly ApplicationDbContext _dbContext;
    private readonly IMemoryCache _memoryCache;

    public ClientAuthorizationService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        RoleManager<Role> roleManager,
        IOptions<AuthConfiguration> authOptions,
        TokenValidationParameters tokenValidationParameters,
        ApplicationDbContext dbContext,
        IMemoryCache memoryCache)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _authOptions = authOptions;
        _tokenValidationParameters = tokenValidationParameters;
        _dbContext = dbContext;
        _memoryCache = memoryCache;
    }

    public async Task<Result<LoginResponse>> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return Result<LoginResponse>.Failure(ErrorUserNotFound, "User not found");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded)
        {
            return Result<LoginResponse>.Failure(ErrorInvalidCredentials, "Invalid credentials");
        }

        var (token, refreshToken) = await GenerateJwtAndRefreshTokenAsync(user, null);

        return Result<LoginResponse>.Success(new LoginResponse(token, refreshToken));
    }

    public async Task<Result<RefreshTokenResponse>> RefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
    {
        var validatedToken = GetPrincipalFromToken(token, _tokenValidationParameters);
        if (validatedToken is null)
        {
            return Result<RefreshTokenResponse>.Failure(ErrorInvalidToken, "Invalid token");
        }

        var jti = validatedToken.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;
        if (string.IsNullOrEmpty(jti))
        {
            return Result<RefreshTokenResponse>.Failure(ErrorInvalidToken, "Invalid token");
        }

        var storedRefreshToken = await _dbContext.Set<RefreshToken>().FirstOrDefaultAsync(x => x.Token == refreshToken, cancellationToken);
        if (storedRefreshToken is null)
        {
            return Result<RefreshTokenResponse>.Failure(ErrorInvalidToken, "This refresh token does not exist");
        }

        if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
        {
            return Result<RefreshTokenResponse>.Failure(ErrorInvalidToken, "This refresh token has expired");
        }

        if (storedRefreshToken.Invalidated)
        {
            return Result<RefreshTokenResponse>.Failure(ErrorInvalidToken, "This refresh token has been invalidated");
        }

        if (storedRefreshToken.JwtId != jti)
        {
            return Result<RefreshTokenResponse>.Failure(ErrorInvalidToken, "This refresh token does not match this JWT");
        }

        var userId = validatedToken.Claims.FirstOrDefault(x => x.Type == "userid")?.Value;
        if (userId is null)
        {
            return Result<RefreshTokenResponse>.Failure(ErrorUserNotFound, "Current user is not found");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return Result<RefreshTokenResponse>.Failure(ErrorUserNotFound, "Current user is not found");
        }

        var (newToken, newRefreshToken) = await GenerateJwtAndRefreshTokenAsync(user, refreshToken);
        return Result<RefreshTokenResponse>.Success(new RefreshTokenResponse(newToken, newRefreshToken));
    }

    private async Task<(string token, string refreshToken)> GenerateJwtAndRefreshTokenAsync(User user, string? existingRefreshToken)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var userRole = roles.FirstOrDefault() ?? "user";

        var role = await _roleManager.FindByNameAsync(userRole);
        var roleClaims = role is not null ? await _roleManager.GetClaimsAsync(role) : [];

        var token = GenerateJwtToken(user, _authOptions.Value, userRole, roleClaims);
        var refreshToken = await GenerateRefreshTokenAsync(token, user, existingRefreshToken);

        return (token, refreshToken);
    }

    private async Task<string> GenerateRefreshTokenAsync(string token, User user, string? existingRefreshToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        var jti = jwtToken.Id;

        var refreshToken = new RefreshToken
        {
            Token = Guid.NewGuid().ToString(),
            JwtId = jti,
            UserId = user.Id,
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            CreatedAtUtc = DateTime.UtcNow,
        };

        if (!string.IsNullOrEmpty(existingRefreshToken))
        {
	        var existingToken = await _dbContext.Set<RefreshToken>().FirstOrDefaultAsync(x => x.Token == existingRefreshToken);
	        if (existingToken != null)
	        {
		        _dbContext.Set<RefreshToken>().Remove(existingToken);
	        }
        }

        await _dbContext.AddAsync(refreshToken);
        await _dbContext.SaveChangesAsync();

        return refreshToken.Token;
    }

    private static string GenerateJwtToken(User user,
        AuthConfiguration authConfiguration,
        string userRole,
        IList<Claim> roleClaims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfiguration.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenId = Guid.NewGuid().ToString();
        List<Claim> claims = [
            new(JwtRegisteredClaimNames.Sub, user.Email!),
            new("userid", user.Id),
            new("role", userRole),
            new(JwtRegisteredClaimNames.Jti, tokenId)
        ];

        foreach (var roleClaim in roleClaims)
        {
            claims.Add(new Claim(roleClaim.Type, roleClaim.Value));
        }

        var token = new JwtSecurityToken(
            issuer: authConfiguration.Issuer,
            audience: authConfiguration.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static ClaimsPrincipal? GetPrincipalFromToken(string token, TokenValidationParameters parameters)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var tokenValidationParameters = parameters.Clone();
            tokenValidationParameters.ValidateLifetime = false;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
            return IsJwtWithValidSecurityAlgorithm(validatedToken) ? principal : null;
        }
        catch
        {
            return null;
        }
    }

    private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        => validatedToken is JwtSecurityToken jwtSecurityToken
           && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

    /// <summary>
    /// Updates a user's role and invalidates their refresh tokens
    /// </summary>
    /// <param name="userId">The ID of the user to update</param>
    /// <param name="newRole">The new role to assign to the user</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns>A result containing success or failure information</returns>
    public async Task<Result<string>> UpdateUserRoleAsync(string userId, string newRole, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(newRole))
        {
            return Result<string>.Failure(ErrorInvalidInput, "Role cannot be empty");
        }

        // Verify the role exists
        var role = await _roleManager.FindByNameAsync(newRole);
        if (role == null)
        {
            return Result<string>.Failure(ErrorInvalidInput, $"Role '{newRole}' does not exist");
        }

        // Find the user
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Result<string>.Failure(ErrorUserNotFound, $"User with ID '{userId}' not found");
        }

        // Get current roles and remove them
        var currentRoles = await _userManager.GetRolesAsync(user);
        if (currentRoles.Any())
        {
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
        }

        // Add the new role
        var addRoleResult = await _userManager.AddToRoleAsync(user, newRole);
        if (!addRoleResult.Succeeded)
        {
            var errors = string.Join(", ", addRoleResult.Errors.Select(e => e.Description));
            return Result<string>.Failure(ErrorInvalidInput, $"Failed to add role: {errors}");
        }

        // Invalidate all refresh tokens for this user
        var refreshTokens = await _dbContext.RefreshTokens
            .Where(rt => rt.UserId == userId && !rt.Invalidated)
            .ToListAsync(cancellationToken);

        foreach (var refreshToken in refreshTokens)
        {
            refreshToken.Invalidated = true;
            refreshToken.UpdatedAtUtc = DateTime.UtcNow;

            // Add to memory cache for the middleware to check
            _memoryCache.Set(refreshToken.JwtId, RevocatedTokenType.RoleChanged);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result<string>.Success($"User role updated to {newRole}");
    }
}
