using System.Security.Claims;
using AspNetCoreIdentityEfCore.Api.Features.Authors.Shared;
using Carter;
using Domain.Authors;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityEfCore.Api.Features.Authors;

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
		BooksDbContext context,
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
