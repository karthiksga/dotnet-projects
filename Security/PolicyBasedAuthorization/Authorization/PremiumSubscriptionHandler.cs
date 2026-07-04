using Microsoft.AspNetCore.Authorization;
using PolicyBasedAuthorization.Entities;

namespace PolicyBasedAuthorization.Authorization;

public class PremiumSubscriptionHandler : AuthorizationHandler<ProUserRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProUserRequirement requirement)
    {
        if (context.User.HasClaim(AppClaimTypes.Subscription, "premium"))
        {
            context.Succeed(requirement);
        }

        // Never call Fail() just because THIS handler didn't match -
        // another handler for the same requirement may still succeed.
        return Task.CompletedTask;
    }
}
