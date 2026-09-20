using MediatR;
using NewsletterApi.Behaviors;
using NewsletterApi.Commands;

namespace NewsletterApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            builder.Services.AddAuthorization();

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapPost("/register", async (IMediator mediator, RegisterUserRequest command) =>
            {
                var response = await mediator.Send(command);
                return Results.Ok(response);
            });

            app.Run();
        }
    }
}
