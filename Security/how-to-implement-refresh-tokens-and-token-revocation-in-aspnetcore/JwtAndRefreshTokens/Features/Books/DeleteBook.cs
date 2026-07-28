using Carter;
using JwtAndRefreshTokens.Database;
using Microsoft.AspNetCore.Mvc;

namespace JwtAndRefreshTokens.Features.Books;

public class DeleteBookEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapDelete("/api/books/{id}", Handle)
			.RequireAuthorization("books:delete");
			//.RequireAuthorization("BookEditor");
	}

	private static async Task<IResult> Handle(
		[FromRoute] Guid id,
		ApplicationDbContext context,
		CancellationToken cancellationToken)
	{
		var book = await context.Books.FindAsync([id], cancellationToken);
		if (book is null)
		{
			return Results.NotFound();
		}

		context.Books.Remove(book);
		await context.SaveChangesAsync(cancellationToken);

		return Results.NoContent();
	}
}

