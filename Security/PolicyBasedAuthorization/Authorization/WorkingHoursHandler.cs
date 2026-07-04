using Microsoft.AspNetCore.Authorization;

namespace PolicyBasedAuthorization.Authorization;

public class WorkingHoursHandler(TimeProvider timeProvider) : AuthorizationHandler<WorkingHoursRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, WorkingHoursRequirement requirement)
    {
        var hour = timeProvider.GetUtcNow().Hour;

        //if (hour >= requirement.StartHourUtc && hour < requirement.EndHourUtc)
        //{
        //    context.Succeed(requirement);
        //}

        if(hour< 13)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
