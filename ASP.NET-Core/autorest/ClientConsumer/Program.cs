using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Weatherforecast;
using Weatherforecast.Weatherforecast;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", async () =>
{
    //var forecast = Enumerable.Range(1, 5).Select(index =>
    //    new WeatherForecast
    //    (
    //        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
    //        Random.Shared.Next(-20, 55),
    //        summaries[Random.Shared.Next(summaries.Length)]
    //    ))
    //    .ToArray();
    //return forecast;

    // 1. Configure Authentication (Use Anonymous if your API doesn't require keys yet)
    var authProvider = new AnonymousAuthenticationProvider();

    // 2. Setup the HTTP Request Adapter with your target API base URL
    var requestAdapter = new HttpClientRequestAdapter(authProvider)
    {
        BaseUrl = "https://localhost:7213" // Change to your actual backend port
    };


    var apiClient = new ApiClient(requestAdapter);
    try
    {
        // Executes: GET https://localhost:7213/weatherforecast
        var forecasts = await apiClient.Weatherforecast.GetAsync();

        if (forecasts != null)
        {
            foreach (var day in forecasts)
            {
                Console.WriteLine($"{day.Date?.ToString()}: {day.TemperatureC}°C - {day.Summary}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"API Request failed: {ex.Message}");
    }
});

app.Run();

//internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}
