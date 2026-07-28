using System.Security.Claims;
using Carter;
using JwtAndRefreshTokens.Authorization;
using JwtAndRefreshTokens.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JwtAndRefreshTokens.Features.Books;

public sealed record UpdateBookRequest(Guid Id, string Title, int Year, Guid AuthorId);

public class UpdateBookEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPut("/api/books/{id}", Handle)
			.RequireAuthorization("books:update");
			//.RequireAuthorization("BookEditor");
	}

	private static async Task<IResult> Handle(
		[FromRoute] Guid id,
		[FromBody] UpdateBookRequest request,
		ApplicationDbContext context,
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

		var requirement = new BookAuthorRequirement();

		var authResult = await authService.AuthorizeAsync(user, book.Author, requirement);
		if (!authResult.Succeeded)
		{
			return Results.Forbid();
		}

		book.Title = request.Title;
		book.Year = request.Year;

		await context.SaveChangesAsync(cancellationToken);

		return Results.NoContent();
	}
}
