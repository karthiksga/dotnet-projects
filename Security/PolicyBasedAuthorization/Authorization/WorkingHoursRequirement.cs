using Microsoft.AspNetCore.Authorization;

namespace PolicyBasedAuthorization.Authorization;

// Requirements carry DATA for the rule. This one says: only between these hours (UTC).
public class WorkingHoursRequirement(int startHourUtc, int endHourUtc) : IAuthorizationRequirementWithMessage
{
    public int StartHourUtc { get; } = startHourUtc;
    public int EndHourUtc { get; } = endHourUtc;

    public string GetFailureMessage()
    {
        return $"Documents can only be accessed between {StartHourUtc:00}:00 and {EndHourUtc:00}:00 UTC.";
    }
}
