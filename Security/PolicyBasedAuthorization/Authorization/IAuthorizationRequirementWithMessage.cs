using Microsoft.AspNetCore.Authorization;

namespace PolicyBasedAuthorization.Authorization;

public interface IAuthorizationRequirementWithMessage: IAuthorizationRequirement
{
    /// <summary>
    /// Gets the error message to display when this requirement fails.
    /// </summary>
    string GetFailureMessage();
}
