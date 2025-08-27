using System.Security.Claims;
using Carter;
using Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentityEfCore.Api.Features.Books;

public sealed record UpdateBookRequest(Guid Id, string Title, int Year, Guid AuthorId);

public class UpdateBookEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPut("/api/books/{id}", Handle)
			.RequireAuthorization("books:update");
	}

	private static async Task<IResult> Handle(
		[FromRoute] Guid id,
		[FromBody] UpdateBookRequest request,
		BooksDbContext context,
		IAuthorizationService authService,
		ClaimsPrincipal user,
		CancellationToken cancellationToken)
	{
		var book = await context.Books
			.Include(x => x.Author)
			.Where(x => x.Id == id)
			.Where(x => x.AuthorId == request.AuthorId)
			.FirstOrDefaultAsync(cancellationToken);

		if (book is null)
		{
			return Results.NotFound($"Book with id {id} not found");
		}

		var userId = user.FindFirst("userid")?.Value;
		if (userId is null || Guid.Parse(userId) != book.Author.Id)
		{
			return Results.Forbid();
		}

		book.Title = request.Title;
		book.Year = request.Year;

		await context.SaveChangesAsync(cancellationToken);

		return Results.NoContent();
	}
}
