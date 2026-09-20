using Carter;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityEfCore.Api.Features.Users;

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
		BooksDbContext context,
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
