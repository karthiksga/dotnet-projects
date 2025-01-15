using JwtAuthApp.Configurations;
using JwtAuthApp.JWT;
using JwtAuthApp.Model;
using JwtAuthApp.Services;
using Microsoft.AspNetCore.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<JwtConfiguration>();
builder.Services.AddTransient<TokenService>();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddTransient<AppUser>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(SwaggerConfiguration.Configure);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.RequireAuthorization()
.WithName("GetWeatherForecast")
.WithOpenApi();

//Inject AppUser and get user email from the token 
app.MapGet("/user", (AppUser user) =>
{
    return Results.Ok(user.Email);
})
.RequireAuthorization()
.WithName("GetUserEmail")
.WithOpenApi();

app.MapPost("/login", (LoginRequest request, TokenService tokenService) =>
{
    // In a real app, you would validate the user's credentials against a database.
    // Authenticate user and generate token 
    // For demo purposes, we are using hardcoded values
    var userIsAuthenticated = request.Email == "admin" && request.Password == "admin";

    if (!userIsAuthenticated)
    {
        return Results.Unauthorized();
    }
    var userId = "9999"; // Get user id from database
    var email = "valentin.osidach@gmail.com"; // Get email from database
    var token = tokenService.GenerateToken(userId, email);

    return Results.Ok(token);
}).AllowAnonymous();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
