using JwtAuthentication.Auth;
using JwtAuthentication.Data;
using JwtAuthentication.Entities;
using JwtAuthentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthentication.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth").WithTags("Auth");

        group.MapPost("/register", RegisterAsync);
        group.MapPost("/login", LoginAsync);

        // Only an existing admin is allowed to hand out roles.
        group.MapPost("/add-role", AddRoleAsync)
            .RequireAuthorization(policy => policy.RequireRole(Roles.Admin));

        // Refresh and revoke are anonymous on purpose: the access token is usually
        // expired by the time you call them, so the refresh token IS the credential.
        group.MapPost("/refresh", RefreshAsync);
        group.MapPost("/revoke", RevokeAsync);
    }

    private static async Task<IResult> RegisterAsync(
        RegisterRequest request,
        UserManager<ApplicationUser> userManager)
    {
        // Stop duplicate sign-ups with the same email.
        if (await userManager.FindByEmailAsync(request.Email) is not null)
        {
            return Results.BadRequest($"Email '{request.Email}' is already registered.");
        }

        var user = new ApplicationUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.Email,
            Email = request.Email
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            // Identity tells us exactly why (weak password, etc.).
            return Results.BadRequest(result.Errors.Select(e => e.Description));
        }

        // Every new user starts as a regular "User".
        await userManager.AddToRoleAsync(user, Roles.User);

        return Results.Ok($"User '{request.Email}' registered successfully.");
    }

    private static async Task<IResult> LoginAsync(
        LoginRequest request,
        AppDbContext db,
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService,
        HttpContext httpContext,
        IOptions<JwtSettings> jwtSettings)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        // Same response for "no such user" and "wrong password" so we don't leak which emails exist.
        if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            return Results.Unauthorized();
        }

        var roles = await userManager.GetRolesAsync(user);
        var (token, expiresAt) = tokenService.CreateToken(user, roles);

        // Rotation: every refresh kills the old token and issues a brand-new one.
        var newRefreshToken = tokenService.CreateRefreshToken();
        var refreshExpiresAt = DateTime.UtcNow.AddDays(jwtSettings.Value.RefreshTokenDays);

        db.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            UserId = user.Id,
            Created = DateTime.UtcNow,
            Expires = refreshExpiresAt
        });
        await db.SaveChangesAsync();

        // Set refresh token as httpOnly cookie
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,              // Cannot be accessed by JavaScript
            Secure = true,                // Only sent over HTTPS
            SameSite = SameSiteMode.Strict, // CSRF protection
            Expires = refreshExpiresAt // Match your refresh token lifetime
        };

        httpContext.Response.Cookies.Append("refreshToken", newRefreshToken, cookieOptions);

        return Results.Ok(new AuthResponse(user.Id, user.Email!,roles, token, expiresAt,newRefreshToken,refreshExpiresAt));
    }

    private static async Task<IResult> AddRoleAsync(
        AddRoleRequest request,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return Results.NotFound($"No user found with email '{request.Email}'.");
        }

        if (!await roleManager.RoleExistsAsync(request.Role))
        {
            return Results.BadRequest($"Role '{request.Role}' does not exist.");
        }

        await userManager.AddToRoleAsync(user, request.Role);

        // Important: the user's CURRENT token still carries the old roles.
        // They need to log in again for the new role to take effect.
        return Results.Ok($"Role '{request.Role}' added to '{request.Email}'. Log in again to refresh the token.");
    }

    private static async Task<IResult> RefreshAsync(
       RefreshRequest request,
       AppDbContext db,
       UserManager<ApplicationUser> userManager,
       ITokenService tokenService,
       HttpContext httpContext,
       IOptions<JwtSettings> jwtSettings)
    {
        // Read refresh token from httpOnly cookie      
        if (!httpContext.Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
        {
            return Results.Problem(
                detail: "Refresh token not found", 
                statusCode: StatusCodes.Status401Unauthorized);
        }

        var existing = await db.RefreshTokens
            .FirstOrDefaultAsync(t => t.Token == refreshToken);

        // Token we have never seen - reject.
        if (existing is null)
        {
            return Results.Unauthorized();
        }

        // Token exists but is not usable. If it was already revoked, someone is
        // replaying an old token - assume it was stolen and revoke the whole family.
        if (!existing.IsActive)
        {
            if (existing.Revoked is not null)
            {
                await RevokeAllActiveTokensAsync(db, existing.UserId);
            }
            return Results.Unauthorized();
        }

        var user = await userManager.FindByIdAsync(existing.UserId);
        if (user is null)
        {
            return Results.Unauthorized();
        }

        // Rotation: every refresh kills the old token and issues a brand-new one.
        var newRefreshToken = tokenService.CreateRefreshToken();
        var refreshExpiresAt = DateTime.UtcNow.AddDays(jwtSettings.Value.RefreshTokenDays);

        existing.Revoked = DateTime.UtcNow;
        existing.ReplacedByToken = newRefreshToken;

        db.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            UserId = user.Id,
            Created = DateTime.UtcNow,
            Expires = refreshExpiresAt
        });
        await db.SaveChangesAsync();

        var roles = await userManager.GetRolesAsync(user);
        var (accessToken, accessExpiresAt) = tokenService.CreateToken(user, roles);

        // Update the cookie with new refresh token
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = refreshExpiresAt
        };

        httpContext.Response.Cookies.Append("refreshToken", newRefreshToken, cookieOptions);

        return Results.Ok(new AuthResponse(
            user.Id, user.Email!, roles,
            accessToken, accessExpiresAt,
            newRefreshToken, refreshExpiresAt));
    }

    private static async Task<IResult> RevokeAsync(RevokeRequest request, AppDbContext db, HttpContext httpContext)
    {
        var token = await db.RefreshTokens
            .FirstOrDefaultAsync(t => t.Token == request.RefreshToken);

        if (token is null || !token.IsActive)
        {
            return Results.NotFound("Token not found or already inactive.");
        }

        token.Revoked = DateTime.UtcNow;
        await db.SaveChangesAsync();
        // Delete the cookie
        httpContext.Response.Cookies.Delete("refreshToken");
        return Results.Ok("Refresh token revoked.");
    }

    // When a stolen token is detected, cancel every active token for that user
    // so the attacker (and the real user) both have to log in again.
    private static async Task RevokeAllActiveTokensAsync(AppDbContext db, string userId)
    {
        var activeTokens = await db.RefreshTokens
            .Where(t => t.UserId == userId && t.Revoked == null)
            .ToListAsync();

        foreach (var token in activeTokens)
        {
            token.Revoked = DateTime.UtcNow;
        }

        await db.SaveChangesAsync();
    }
}
