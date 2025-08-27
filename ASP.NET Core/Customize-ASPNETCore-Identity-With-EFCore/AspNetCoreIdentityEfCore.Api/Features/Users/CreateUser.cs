using AspNetCoreIdentityEfCore.Api.Features.Users.Shared;
using Carter;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityEfCore.Api.Features.Users;

public sealed record CreateUserRequest(string Email, string Password, string? Role);

public class CreateUserEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/api/users", Handle)
			.RequireAuthorization("users:create");
			//.RequireAuthorization("Admin");
	}

	private static async Task<IResult> Handle(
		[FromBody] CreateUserRequest request,
		UserManager<User> userManager,
		CancellationToken cancellationToken)
	{
		var user = new User
		{
			Id = Guid.NewGuid().ToString(),
			Email = request.Email,
			UserName = request.Email
		};

		var result = await userManager.CreateAsync(user, request.Password);

		if (!string.IsNullOrWhiteSpace(request.Role))
		{
			await userManager.AddToRoleAsync(user, request.Role);
		}

		if (!result.Succeeded)
		{
			return Results.BadRequest(result.Errors);
		}


		var response = new UserResponse(user.Id, user.Email);
		return Results.Created($"/api/users/{user.Id}", response);
	}
}

