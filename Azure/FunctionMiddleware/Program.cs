using FunctionMiddleware.Middlewares;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);
builder.Configuration
    .AddEnvironmentVariables();
builder
    .UseMiddleware<UppercaseNameMiddleware>()
    .UseMiddleware<ExceptionHandlerMiddleware>();
var host = builder.Build();

await host.RunAsync();

/*Original Implementation*/
//var host = new HostBuilder()
//    .ConfigureAppConfiguration(builder =>
//        builder.AddEnvironmentVariables())
//    .ConfigureFunctionsWebApplication(worker =>
//    {
//        // Middleware Sequence
//        worker.UseMiddleware<UppercaseNameMiddleware>();
//        worker.UseMiddleware<ExceptionHandlerMiddleware>();
//    })
//    .ConfigureServices(services =>
//    {
//        services.AddApplicationInsightsTelemetryWorkerService();
//        services.ConfigureFunctionsApplicationInsights();
//    })
//    .Build();

//await host.RunAsync();