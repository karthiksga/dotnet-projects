using Carter;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityEfCore.Api.Features.Users;

public sealed record UpdateUserRequest(Guid Id, string Email);

public class UpdateUserEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPut("/api/users/{id}", Handle)
			.RequireAuthorization("users:update");
			//.RequireAuthorization("Admin");
	}

	private static async Task<IResult> Handle(
		[FromRoute] Guid id,
		[FromBody] UpdateUserRequest request,
		BooksDbContext context,
		CancellationToken cancellationToken)
	{
		var user = await context.Users.FindAsync([id], cancellationToken);
		if (user is null)
		{
			return Results.NotFound();
		}

		user.Email = request.Email;

		await context.SaveChangesAsync(cancellationToken);

		return Results.NoContent();
	}
}

