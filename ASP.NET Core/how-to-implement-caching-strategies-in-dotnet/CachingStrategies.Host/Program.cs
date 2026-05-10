using CachingStrategies.Host.Services;
using CachingStrategies.Infrastructure.Database;
using Carter;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
	options.CustomSchemaIds(type => type.FullName);
});

builder.Services.AddWebHostInfrastructure(builder.Configuration);
builder.Services.AddCarter();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCarter();

// Create and seed database
using (var scope = app.Services.CreateScope())
{
	// To create migrations run the command:
	// dotnet ef migrations add InitialPostres --startup-project ./CachingStrategies.Host --project ./CachingStrategies.Infrastructure -- --context CachingStrategies.Infrastructure.Database.ApplicationDbContext
	
	var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
	await dbContext.Database.MigrateAsync();
	await DatabaseSeedService.SeedAsync(dbContext);
}
await app.RunAsync();
