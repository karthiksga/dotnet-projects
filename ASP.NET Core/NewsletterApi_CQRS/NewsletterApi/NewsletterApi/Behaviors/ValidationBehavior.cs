using MediatR;
using NewsletterApi.Commands;

namespace NewsletterApi.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Hello from the Validation Behavior 1st time!");

        if (request is RegisterUserRequest registerUser)
        {
            if (string.IsNullOrWhiteSpace(registerUser.Username) ||
                string.IsNullOrWhiteSpace(registerUser.Email) ||
                string.IsNullOrWhiteSpace(registerUser.Password))
            {
                throw new ArgumentException("❌ Username, Email, and Password are required.");
            }
        }

        var response = await next();

        Console.WriteLine($"Hello from the Validation Behavior 2nd time!");

        return response;
    }
}