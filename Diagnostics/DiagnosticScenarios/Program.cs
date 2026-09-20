using DiagnosticScenarios.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add<LogRequestTimeFilterAttribute>();
    })
    .AddNewtonsoftJson();


var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
