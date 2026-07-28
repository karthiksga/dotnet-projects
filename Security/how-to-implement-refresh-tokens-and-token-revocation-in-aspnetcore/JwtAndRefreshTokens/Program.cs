using Carter;
using JwtAndRefreshTokens.Database;
using JwtAndRefreshTokens.Database.Entities;
using JwtAndRefreshTokens.Extensions;
using JwtAndRefreshTokens.Middlewares;
using JwtAndRefreshTokens.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CheckRevocatedTokensMiddleware>();

app.MapCarter();

// Create and seed database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

    await DatabaseSeedService.SeedAsync(dbContext, userManager, roleManager);
}

await app.RunAsync();
