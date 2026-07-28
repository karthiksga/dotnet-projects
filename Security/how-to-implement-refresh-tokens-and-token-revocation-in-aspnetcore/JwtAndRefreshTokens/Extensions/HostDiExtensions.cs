using System.Text;
using Carter;
using JwtAndRefreshTokens.Authorization;
using JwtAndRefreshTokens.Configuration;
using JwtAndRefreshTokens.Database;
using JwtAndRefreshTokens.Database.Entities;
using JwtAndRefreshTokens.Features.Authorization;
using JwtAndRefreshTokens.HostedServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JwtAndRefreshTokens.Extensions;

public static class HostDiExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHttpContextAccessor();
        services.AddMemoryCache();
        services.AddCarter();

        services.AddSingleton<AuditableInterceptor>();
        services.AddScoped<IClientAuthorizationService, ClientAuthorizationService>();
        services.AddHostedService<InvalidatedTokensHostedService>();

        services.AddEfCore(configuration);
        services.AddIdentityServices();
        services.AddAuthServices(configuration);

        return services;
    }

    private static IServiceCollection AddEfCore(this IServiceCollection services, IConfiguration configuration)
    {
	    var connectionString = configuration.GetConnectionString("Postgres");

	    services.AddDbContext<ApplicationDbContext>((provider, options) =>
	    {
		    var interceptor = provider.GetRequiredService<AuditableInterceptor>();

		    options.EnableSensitiveDataLogging()
			    .UseNpgsql(connectionString, npgsqlOptions =>
			    {
				    npgsqlOptions.MigrationsHistoryTable(DatabaseConsts.MigrationTableName, DatabaseConsts.Schema);
			    })
			    .AddInterceptors(interceptor)
			    .UseSnakeCaseNamingConvention();
	    });

        return services;
    }

    private static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
	    services
		    .AddIdentity<User, Role>(options =>
		    {
			    options.Password.RequireDigit = true;
			    options.Password.RequireLowercase = true;
			    options.Password.RequireUppercase = true;
			    options.Password.RequireNonAlphanumeric = true;
			    options.Password.RequiredLength = 8;
		    })
		    .AddEntityFrameworkStores<ApplicationDbContext>()
		    .AddSignInManager()
		    .AddDefaultTokenProviders();

        return services;
    }

    private static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
    {
	    services.AddOptions<AuthConfiguration>()
		    .Bind(configuration.GetSection(nameof(AuthConfiguration)));

	    var tokenValidationParameters = new TokenValidationParameters
	    {
		    ValidateIssuer = true,
		    ValidateAudience = true,
		    ValidateLifetime = true,
		    ValidateIssuerSigningKey = true,
		    ValidIssuer = configuration["AuthConfiguration:Issuer"],
		    ValidAudience = configuration["AuthConfiguration:Audience"],
		    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthConfiguration:Key"]!))
	    };

	    services.AddSingleton(tokenValidationParameters);

	    services.AddAuthentication(options =>
		    {
			    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		    })
		    .AddJwtBearer(options =>
		    {
			    options.TokenValidationParameters = tokenValidationParameters;
		    });

	    services.AddAuthorization(options =>
	    {
		    options.AddPolicy("Admin", policy =>
		    {
			    policy.RequireRole("Admin");
		    });

		    options.AddPolicy("Author", policy =>
		    {
			    policy.RequireRole("Author");
		    });

		    options.AddPolicy("BookEditor", policy =>
		    {
			    // Allow both Admin and Author roles to edit books
			    policy.RequireRole("Admin", "Author");
		    });

		    options.AddPolicy("books:create", policy => policy.RequireClaim("books:create"));
		    options.AddPolicy("books:update", policy => policy.RequireClaim("books:update"));
		    options.AddPolicy("books:delete", policy => policy.RequireClaim("books:delete"));

		    options.AddPolicy("users:create", policy => policy.RequireClaim("users:create", "true"));
		    options.AddPolicy("users:update", policy => policy.RequireClaim("users:update"));
		    options.AddPolicy("users:delete", policy => policy.RequireClaim("users:delete"));
	    });

	    services.AddScoped<IAuthorizationHandler, BookAuthorHandler>();

        return services;
    }
}
