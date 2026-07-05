using ClaimsBasedAuthorization.Entities;
using ClaimsBasedAuthorization.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Caching.Hybrid;
using System.Data;
using System.Security.Claims;
using System.Threading.Channels;

namespace ClaimsBasedAuthorization.Auth;

// Runs AFTER token validation on every request. Enriches the ClaimsPrincipal with
// claims that should NOT live inside the token - here, the subscription tier,
// which can change at any moment and must always be current.
public class SubscriptionClaimsTransformation(SubscriptionStore subscriptions) : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        // Never add the same claim twice - transformations can run more than once per request.
        if (principal.HasClaim(c => c.Type == AppClaimTypes.Subscription))
        {
            return Task.FromResult(principal);
        }

        var userId = principal.FindFirstValue("sub");
        if (userId is null)
        {
            return Task.FromResult(principal);
        }

        var tier = subscriptions.GetTier(userId);

        // Clone before modifying - the incoming principal should be treated as read-only.
        // the clone-before-modify pattern, which avoids mutating a principal other middleware may hold a reference to.
        var clone = principal.Clone();
        var identity = (ClaimsIdentity)clone.Identity!;
        identity.AddClaim(new Claim(AppClaimTypes.Subscription, tier));

        /**
        My decision rule for token vs transformation: 
            stable identity facts go in the token, 
            volatile entitlement facts go through transformation.
        Department changes once a year -token.
        Subscription changes on a failed payment - transformation.
        And weigh the cost honestly: 
            a transformation hitting your database runs on every request to every protected endpoint, 
            so back it with HybridCache or accept the latency.
        **/

        return Task.FromResult(clone);
    }
}