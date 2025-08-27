using Microsoft.EntityFrameworkCore;
using VerticalSlicesArchitecture.Database;

namespace VerticalSlicesArchitecture.Extensions;

public static class DatabaseExtensions
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserDbContext>(context =>
                    context.UseSqlServer(configuration.GetConnectionString("Database")));
    }
}