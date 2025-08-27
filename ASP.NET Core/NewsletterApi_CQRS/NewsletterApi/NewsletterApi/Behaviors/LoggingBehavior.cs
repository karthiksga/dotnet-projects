using MediatR;

namespace NewsletterApi.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        //Before handler
        _logger.LogInformation($"Handling {typeof(TRequest).Name}");

        Console.WriteLine($"Hello from the Logging Behavior 1st time!");

        var response = await next();

        //After handler
        _logger.LogInformation($"Handled {typeof(TResponse).Name}");

        Console.WriteLine($"Hello from the Logging Behavior 2nd time!");

        return response;
    }
}
