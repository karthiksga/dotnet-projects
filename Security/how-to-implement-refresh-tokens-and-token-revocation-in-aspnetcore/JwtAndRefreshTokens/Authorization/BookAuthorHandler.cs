using JwtAndRefreshTokens.Database.Entities;
using Microsoft.AspNetCore.Authorization;

namespace JwtAndRefreshTokens.Authorization;

public class BookAuthorHandler : AuthorizationHandler<BookAuthorRequirement, Author>
{
	protected override Task HandleRequirementAsync(
		AuthorizationHandlerContext context,
		BookAuthorRequirement requirement,
		Author resource)
	{
		var userId = context.User.FindFirst("userid")?.Value;
		if (userId is not null && userId == resource.UserId)
		{
			context.Succeed(requirement);
		}

		return Task.CompletedTask;
	}
}
