using System.Security.Claims;
using Carter;
using JwtAndRefreshTokens.Database;
using JwtAndRefreshTokens.Database.Entities;
using JwtAndRefreshTokens.Features.Authors.Shared;
using Microsoft.AspNetCore.Mvc;

namespace JwtAndRefreshTokens.Features.Authors;

public sealed record CreateAuthorRequest(string Name);

public class CreateAuthorEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/api/authors", Handle)
			.RequireAuthorization();
	}

	private static async Task<IResult> Handle(
		[FromBody] CreateAuthorRequest request,
		ApplicationDbContext context,
		ClaimsPrincipal user,
		CancellationToken cancellationToken)
	{
		var author = new Author
		{
			Id = Guid.NewGuid(),
			Name = request.Name,
			UserId = user.FindFirst("userid")?.Value
		};

		context.Authors.Add(author);
		await context.SaveChangesAsync(cancellationToken);

		var response = new AuthorResponse(author.Id, author.Name, []);

		return Results.Created($"/api/authors/{author.Id}", response);
	}
}
