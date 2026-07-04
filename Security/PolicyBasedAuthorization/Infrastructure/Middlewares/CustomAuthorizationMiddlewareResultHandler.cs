using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using PolicyBasedAuthorization.Authorization;
using System.Text.Json;

namespace PolicyBasedAuthorization.Infrastructure.Middlewares;

/// <summary>
/// Customizes the response for authorization failures to return ProblemDetails with messages from failed requirements.
/// </summary>
public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
    private readonly IAuthorizationMiddlewareResultHandler _defaultHandler = new AuthorizationMiddlewareResultHandler();

    public async Task HandleAsync(
        RequestDelegate next,
        HttpContext context,
        AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult)
    {
        // If authorization succeeded, continue with the default behavior
        if (authorizeResult.Succeeded)
        {
            await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
            return;
        }

        // Authorization failed - customize the response
        if (authorizeResult.Forbidden)
        {
            // User is authenticated but lacks permission (403)
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/problem+json";

            // Collect messages from ALL failed requirements
            var failedRequirements = authorizeResult.AuthorizationFailure?.FailedRequirements ?? [];
            var errorMessages = GetFailureMessages(failedRequirements);

            var problemDetails = new
            {
                type = "https://tools.ietf.org/html/rfc7231#section-6.5.3",
                title = "Forbidden",
                status = StatusCodes.Status403Forbidden,
                detail = errorMessages.Count == 1
                    ? errorMessages[0]
                    : "Multiple authorization requirements failed.",
                errors = errorMessages.Count > 1 ? errorMessages : null,
                instance = context.Request.Path.Value
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            }));
        }
        else if (authorizeResult.Challenged)
        {
            // User is not authenticated (401)
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/problem+json";

            var problemDetails = new
            {
                type = "https://tools.ietf.org/html/rfc7235#section-3.1",
                title = "Unauthorized",
                status = StatusCodes.Status401Unauthorized,
                detail = "You must be authenticated to access this resource.",
                instance = context.Request.Path.Value
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));
        }
        else
        {
            // Fallback to default behavior
            await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }

    private static List<string> GetFailureMessages(IEnumerable<IAuthorizationRequirement> failedRequirements)
    {
        var messages = new List<string>();

        foreach (var requirement in failedRequirements)
        {
            if (requirement is IAuthorizationRequirementWithMessage requirementWithMessage)
            {
                // Requirement provides its own message
                messages.Add(requirementWithMessage.GetFailureMessage());
            }
            else
            {
                // Fallback for requirements that don't implement the interface
                messages.Add($"Authorization requirement '{requirement.GetType().Name}' was not satisfied.");
            }
        }

        // If no messages collected, provide a generic one
        if (messages.Count == 0)
        {
            messages.Add("You do not have permission to access this resource.");
        }

        return messages;
    }
}
