using System.Threading.Channels;
using CachingStrategies.Domain.Products;
using CachingStrategies.Host.Features.Products.CacheAside;
using CachingStrategies.Host.Features.Products.ReadThroughCache;
using CachingStrategies.Host.Features.Products.WriteAround;
using CachingStrategies.Host.Features.Products.WriteBack;
using CachingStrategies.Host.Features.Products.WriteThroughCache;
using CachingStrategies.Infrastructure.Database;
using CachingStrategies.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class HostDiExtensions
{
	public static IServiceCollection AddWebHostInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
#pragma warning disable EXTEXP0018
		services.AddHybridCache(options =>
		{
			options.DefaultEntryOptions = new HybridCacheEntryOptions
			{
				Expiration = TimeSpan.FromMinutes(10),
				LocalCacheExpiration = TimeSpan.FromMinutes(10)
			};
		});
#pragma warning restore EXTEXP0018

		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<IProductCartRepository, ProductCartRepository>();

		services.AddSingleton(_ => Channel.CreateBounded<ProductCartDispatchEvent>(new BoundedChannelOptions(100)
		{
			FullMode = BoundedChannelFullMode.Wait
		}));

		services.AddHostedService<WriteBackCacheBackgroundService>();
		
		services
			.AddScoped<CacheAsideProductService>()
			.AddScoped<ReadThroughCacheProductService>()
			.AddScoped<WriteAroundCacheProductService>()
			.AddScoped<WriteThroughCacheProductService>()
			.AddScoped<WriteBackCacheProductCartService>()
			;
		
		services
			.AddEfCore(configuration)
			.AddRedis(configuration);

		return services;
	}

	private static IServiceCollection AddEfCore(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("Postgres");

		services.AddDbContext<ApplicationDbContext>((_, options) =>
		{
			options.EnableSensitiveDataLogging()
				.UseNpgsql(connectionString, npgsqlOptions =>
				{
					npgsqlOptions.MigrationsHistoryTable(DatabaseConsts.MigrationHistoryTable, DatabaseConsts.Schema);
				});

			options.UseSnakeCaseNamingConvention();
		});

		return services;
	}

	private static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
	{
		var redisConnectionString = configuration.GetConnectionString("Redis")!;

		services.AddMemoryCache();
		
		services
			.AddStackExchangeRedisCache(options =>
			{
				options.Configuration = redisConnectionString;
			});

		return services;
	}
}
