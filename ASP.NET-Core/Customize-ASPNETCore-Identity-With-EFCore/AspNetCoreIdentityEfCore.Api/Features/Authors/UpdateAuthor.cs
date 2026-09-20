using Carter;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentityEfCore.Api.Features.Authors;

public sealed record UpdateAuthorRequest(Guid Id, string Name);

public class UpdateAuthorEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPut("/api/authors/{id}", Handle);
	}

	private static async Task<IResult> Handle(
		[FromRoute] Guid id,
		[FromBody] UpdateAuthorRequest request,
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

		author.Name = request.Name;
		await context.SaveChangesAsync(cancellationToken);

		return Results.NoContent();
	}
}
