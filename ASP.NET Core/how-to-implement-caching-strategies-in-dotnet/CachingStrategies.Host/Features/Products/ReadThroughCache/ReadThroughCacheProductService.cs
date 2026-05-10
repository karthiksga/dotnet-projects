using CachingStrategies.Database.Entities;
using CachingStrategies.Domain.Products;
using CachingStrategies.Host.Features.Shared;
using Microsoft.Extensions.Caching.Hybrid;

namespace CachingStrategies.Host.Features.Products.ReadThroughCache;

public class ReadThroughCacheProductService(HybridCache cache, IProductRepository repository) : IProductService
{
    public async Task<Product?> GetByIdAsync(int id)
    {
        // Define the cache key
        var cacheKey = $"product:{id}";

        // Read-through cache implementation
        var product = await cache.GetOrCreateAsync<Product?>(
            cacheKey,
            // Data loader function to fetch from DB
            async token => await repository.GetByIdAsync(id),
            new HybridCacheEntryOptions
            {
                Expiration = TimeSpan.FromMinutes(10)
            });

        return product;
    }

    public async Task AddAsync(Product product)
    {
        // Add the product to the database
        await repository.AddAsync(product);

        // Update the cache with the new product
        var cacheKey = $"product:{product.Id}";
        
        await cache.SetAsync(cacheKey, product, new HybridCacheEntryOptions
        {
            Expiration = TimeSpan.FromMinutes(10)
        });
    }

    public async Task UpdateAsync(Product product)
    {
        // Update the product in the database
        await repository.UpdateAsync(product);

        // Refresh the product in the cache
        var cacheKey = $"product:{product.Id}";
        
        await cache.SetAsync(cacheKey, product, new HybridCacheEntryOptions
        {
            Expiration = TimeSpan.FromMinutes(10)
        });
    }

    public async Task DeleteAsync(int id)
    {
        // Remove the product from the database
        await repository.DeleteAsync(id);

        // Invalidate the cache entry
        var cacheKey = $"product:{id}";
        await cache.RemoveAsync(cacheKey);
    }
}