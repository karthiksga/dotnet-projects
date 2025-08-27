using System.Text;
using Domain.Users;
using Infrastructure.Configuration;
using Infrastructure.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDatabase(configuration);
		services.AddJwtAuthentication(configuration);
		services.AddClaimsAuthorization();

		return services;
	}

	private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("Postgres");

		services.AddSingleton<AuditableInterceptor>();

		services.AddDbContext<BooksDbContext>((provider, options) =>
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

		services
			.AddIdentity<User, Role>(options =>
			{
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequiredLength = 8;
			})
			.AddEntityFrameworkStores<BooksDbContext>()
			.AddSignInManager()
			.AddDefaultTokenProviders();
	}

	private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddOptions<AuthConfiguration>()
			.Bind(configuration.GetSection(nameof(AuthConfiguration)));

		services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = configuration["AuthConfiguration:Issuer"],
					ValidAudience = configuration["AuthConfiguration:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthConfiguration:Key"]!))
				};
			});

		return services;
	}

	private static IServiceCollection AddClaimsAuthorization(this IServiceCollection services)
	{
		services.AddAuthorization(options =>
		{
			options.AddPolicy("books:create", policy => policy.RequireClaim("books:create"));
			options.AddPolicy("books:update", policy => policy.RequireClaim("books:update"));
			options.AddPolicy("books:delete", policy => policy.RequireClaim("books:delete"));

			options.AddPolicy("users:create", policy => policy.RequireClaim("users:create", "true"));
			options.AddPolicy("users:update", policy => policy.RequireClaim("users:update"));
			options.AddPolicy("users:delete", policy => policy.RequireClaim("users:delete"));
		});

		return services;
	}
}
