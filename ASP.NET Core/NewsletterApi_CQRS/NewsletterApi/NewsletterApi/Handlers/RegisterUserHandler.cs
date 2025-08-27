using MediatR;
using NewsletterApi.Commands;

namespace NewsletterApi.Handlers;

public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, string>
{
    public async Task<string> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        // Simulate user registration (e.g., saving to DB)
        await Task.Delay(500, cancellationToken);

        Console.WriteLine($"Hello from the RegisterUserHandler 1st time!");

        return $"User {request.Username} registered successfully!";
    }
}