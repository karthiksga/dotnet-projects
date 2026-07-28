using Carter;
using JwtAndRefreshTokens.Features.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAndRefreshTokens.Features.Users;

public sealed record RefreshTokenRequest(string Token, string RefreshToken);
public sealed record RefreshTokenResponse(string Token, string RefreshToken);

public class RefreshTokenEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/api/users/refresh", Handle);
	}

	private static async Task<IResult> Handle(
		[FromBody] RefreshTokenRequest request,
		IClientAuthorizationService authorizationService,
		CancellationToken cancellationToken)
	{
		var result = await authorizationService.RefreshTokenAsync(
			request.Token,
			request.RefreshToken,
			cancellationToken);

		if (!result.IsSuccess)
		{
			return Results.Problem(
				statusCode: 400,
				detail: result.Errors?[0].Message,
				title: result.Errors?[0].Code);
		}

		var response = new RefreshTokenResponse(result.Data!.Token, result.Data.RefreshToken);
		return Results.Ok(response);
	}
}
