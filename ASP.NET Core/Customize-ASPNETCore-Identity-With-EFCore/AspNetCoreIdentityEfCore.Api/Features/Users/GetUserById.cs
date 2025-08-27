using AspNetCoreIdentityEfCore.Api.Features.Users.Shared;
using Carter;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentityEfCore.Api.Features.Users;

public class GetUserByIdEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/api/users/{id}", Handle);
	}

	private static async Task<IResult> Handle(
		[FromRoute] string id,
		BooksDbContext context,
		CancellationToken cancellationToken)
	{
		var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
		if (user is null)
		{
			return Results.NotFound();
		}

		var response = new UserResponse(user.Id, user.Email!);
		return Results.Ok(response);
	}
}
