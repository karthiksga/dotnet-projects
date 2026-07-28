using Carter;
using JwtAndRefreshTokens.Features.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAndRefreshTokens.Features.Users;

public sealed record UpdateUserRoleRequest(string NewRole);
public sealed record UpdateUserRoleResponse(bool Success, string Message);

[Authorize(Roles = "admin")]
public class UpdateUserRoleEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/users/{userId}/role", Handle)
            .RequireAuthorization("admin");
    }

    private static async Task<IResult> Handle(
        [FromRoute] string userId,
        [FromBody] UpdateUserRoleRequest request,
        IClientAuthorizationService authorizationService,
        CancellationToken cancellationToken)
    {
        var result = await authorizationService.UpdateUserRoleAsync(userId, request.NewRole, cancellationToken);

        if (!result.IsSuccess)
        {
            if (result.Errors?[0].Code == "user_not_found")
            {
                return Results.NotFound(new UpdateUserRoleResponse(false, result.Errors[0].Message));
            }

            return Results.BadRequest(new UpdateUserRoleResponse(false, result.Errors?[0].Message ?? "An error occurred"));
        }

        return Results.Ok(new UpdateUserRoleResponse(true, result.Data!));
    }
}
