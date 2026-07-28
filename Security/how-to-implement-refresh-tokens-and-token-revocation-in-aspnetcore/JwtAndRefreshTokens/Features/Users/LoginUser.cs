using Carter;
using JwtAndRefreshTokens.Features.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAndRefreshTokens.Features.Users;

public sealed record LoginUserRequest(string Email, string Password);
public sealed record LoginUserResponse(string Token, string RefreshToken);

public class LoginUserEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/api/users/login", Handle);
	}

	private static async Task<IResult> Handle(
		[FromBody] LoginUserRequest request,
		IClientAuthorizationService authorizationService,
		CancellationToken cancellationToken)
	{
		var result = await authorizationService.LoginAsync(request.Email, request.Password, cancellationToken);

		if (!result.IsSuccess)
		{
			return Results.Problem(
				statusCode: 400,
				detail: result.Errors?[0].Message,
				title: result.Errors?[0].Code);
		}

		var response = new LoginUserResponse(result.Data!.Token, result.Data.RefreshToken);
		return Results.Ok(response);
	}
}
