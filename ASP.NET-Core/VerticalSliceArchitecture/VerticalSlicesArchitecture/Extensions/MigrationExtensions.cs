using Microsoft.EntityFrameworkCore;
using VerticalSlicesArchitecture.Database;

namespace VerticalSlicesArchitecture.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();

        dbContext.Database.Migrate();
    }
}