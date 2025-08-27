using Carter;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityEfCore.Api.Features.Books;

public class DeleteBookEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapDelete("/api/books/{id}", Handle)
			.RequireAuthorization("books:delete");
	}

	private static async Task<IResult> Handle(
		[FromRoute] Guid id,
		BooksDbContext context,
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

