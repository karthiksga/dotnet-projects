using Carter;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityEfCore.Api.Features.Authors;

public class DeleteAuthorEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapDelete("/api/authors/{id}", Handle);
	}

	private static async Task<IResult> Handle(
		[FromRoute] Guid id,
		BooksDbContext context,
		CancellationToken cancellationToken)
	{
		var author = await context.Authors.FindAsync([id], cancellationToken);
		if (author is null)
		{
			return Results.NotFound();
		}

		context.Authors.Remove(author);
		await context.SaveChangesAsync(cancellationToken);

		return Results.NoContent();
	}

}
