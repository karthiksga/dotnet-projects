using Microsoft.AspNetCore.Identity;
using PolicyBasedAuthorization.Auth;

//using Microsoft.AspNetCore.Identity.Data;
using PolicyBasedAuthorization.Entities;
using PolicyBasedAuthorization.Models;

namespace PolicyBasedAuthorization.EndPoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        // The app has a FALLBACK policy requiring authentication everywhere -
        // so the auth endpoints must explicitly opt OUT.
        var group = app.MapGroup("/api/auth").WithTags("Auth").AllowAnonymous();

        group.MapPost("/register", RegisterAsync);
        group.MapPost("/login", LoginAsync);
    }

    private static async Task<IResult> RegisterAsync(RegisterRequest request, UserManager<ApplicationUser> userManager)
    {
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
            return Results.BadRequest(result.Errors.Select(e => e.Description));
        }

        await userManager.AddToRoleAsync(user, Roles.User);

        return Results.Ok($"User '{request.Email}' registered successfully.");
    }

    private static async Task<IResult> LoginAsync(LoginRequest request, UserManager<ApplicationUser> userManager, ITokenService tokenService)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            return Results.Unauthorized();
        }
        var roles = await userManager.GetRolesAsync(user);
        var userClaims = await userManager.GetClaimsAsync(user);

        var (token, expiresAt) = tokenService.CreateToken(user, roles, userClaims);
        return Results.Ok(new AuthResponse(
            UserId: user.Id,
            Email: user.Email,
            Roles: roles,
            Token: token,
            ExpiresAt: expiresAt));
    }
}
