using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Carter;
using Domain.Users;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCoreIdentityEfCore.Api.Features.Users;

public sealed record LoginUserRequest(string Email, string Password);

public class LoginUserEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/api/users/login", Handle);
	}

	private static async Task<IResult> Handle(
		[FromBody] LoginUserRequest request,
		IOptions<AuthConfiguration> authOptions,
		UserManager<User> userManager,
		SignInManager<User> signInManager,
		RoleManager<Role> roleManager,
		CancellationToken cancellationToken)
	{
		var user = await userManager.FindByEmailAsync(request.Email);
		if (user is null)
		{
			return Results.NotFound("User not found");
		}

		var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
		if (!result.Succeeded)
		{
			return Results.Unauthorized();
		}

		var roles = await userManager.GetRolesAsync(user);
		var userRole = roles.FirstOrDefault() ?? "user";

		var role = await roleManager.FindByNameAsync(userRole);
		var roleClaims = role is not null ? await roleManager.GetClaimsAsync(role) : [];

		var token = GenerateJwtToken(user, authOptions.Value, userRole, roleClaims);
		return Results.Ok(new { Token = token });
	}

	private static string GenerateJwtToken(User user,
		AuthConfiguration authConfiguration,
		string userRole,
		IList<Claim> roleClaims)
	{
		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfiguration.Key));
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		List<Claim> claims = [
			new(JwtRegisteredClaimNames.Sub, user.Email!),
			new("userid", user.Id),
			new("role", userRole)
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
}
