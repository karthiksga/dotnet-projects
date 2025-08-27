using AspNetCoreIdentityEfCore.Api.Features.Authors.Shared;
using AspNetCoreIdentityEfCore.Api.Features.Books.Shared;
using Carter;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentityEfCore.Api.Features.Authors;

public class GetAuthorByIdEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/api/authors/{id}", Handle);
	}

	private static async Task<IResult> Handle(
		[FromRoute] Guid id,
		BooksDbContext context,
		CancellationToken cancellationToken)
	{
		var author = await context.Authors
			.Include(a => a.Books)
			.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

		if (author is null)
		{
			return Results.NotFound();
		}

		var response = new AuthorResponse(
			author.Id,
			author.Name,
			author.Books
				.Select(b => new BookResponse(b.Id, b.Title, b.Year, b.AuthorId))
				.ToList()
		);

		return Results.Ok(response);
	}
}
