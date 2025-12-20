using GraphQLProductDemo;
using GraphQLProductDemo.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register EF Core InMemory
builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(
    options => options.UseInMemoryDatabase("ProductDb"));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register GraphQL services
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddProjections() // Enables [UseProjection]
    .AddFiltering()   // Enables [UseFiltering]
    .AddSorting();    // Enables [UseSorting]

var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>();
    using var context = dbContextFactory.CreateDbContext();
    context.Database.EnsureCreated();
    context.Products.AddRange(
        new Product { Name = "Shoes", Category = "Footware", Price = 128.99M },
        new Product { Name = "Speaker", Category = "Accessories", Price = 49.49M }
    );
    context.SaveChanges();
}

// Map GraphQL endpoint
app.MapGraphQL();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
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
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
