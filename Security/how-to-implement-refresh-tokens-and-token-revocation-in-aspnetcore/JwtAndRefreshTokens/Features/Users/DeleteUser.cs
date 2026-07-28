using Carter;
using JwtAndRefreshTokens.Database;
using Microsoft.AspNetCore.Mvc;

namespace JwtAndRefreshTokens.Features.Users;

public class DeleteUserEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapDelete("/api/users/{id}", Handle)
			.RequireAuthorization("users:delete");
			//.RequireAuthorization("Admin");
	}

	private static async Task<IResult> Handle(
		[FromRoute] Guid id,
		ApplicationDbContext context,
		CancellationToken cancellationToken)
	{
		var user = await context.Users.FindAsync([id], cancellationToken);
		if (user is null)
		{
			return Results.NotFound();
		}

		context.Users.Remove(user);
		await context.SaveChangesAsync(cancellationToken);

		return Results.NoContent();
	}
}
