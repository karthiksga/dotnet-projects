using Microsoft.AspNetCore.Authorization;
using PolicyBasedAuthorization.Entities;

namespace PolicyBasedAuthorization.Authorization;

public class AdminOverrideHandler : AuthorizationHandler<ProUserRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProUserRequirement requirement)
    {
        if (context.User.IsInRole(Roles.Admin))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
