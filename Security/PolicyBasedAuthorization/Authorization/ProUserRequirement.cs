using Microsoft.AspNetCore.Authorization;

namespace PolicyBasedAuthorization.Authorization;

public class ProUserRequirement : IAuthorizationRequirementWithMessage
{
    public string GetFailureMessage()
    {
        return "User must be a pro subscriber to access this resource.";
    }
}