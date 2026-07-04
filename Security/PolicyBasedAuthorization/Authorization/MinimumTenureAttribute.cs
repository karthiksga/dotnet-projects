using Microsoft.AspNetCore.Authorization;

namespace PolicyBasedAuthorization.Authorization;

// IAuthorizationRequirementData (.NET 8+) lets the ATTRIBUTE carry the requirement.
// No named policy registration needed - [MinimumTenure(6)] just works,
// with any number of months, without registering a policy per value.
public class MinimumTenureAttribute(int months) : AuthorizeAttribute, IAuthorizationRequirementData
{
    public IEnumerable<IAuthorizationRequirement> GetRequirements()
    {
        yield return new MinimumTenureRequirement(months);
    }
}

public class MinimumTenureRequirement(int months) : IAuthorizationRequirementWithMessage
{
    public int Months { get; } = months;

    public string GetFailureMessage()
    {
        return $"User must have a minimum tenure of {Months} months to access this resource.";
    }
}
