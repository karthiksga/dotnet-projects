using CachingStrategies.Database.Entities;
using CachingStrategies.Domain.Products;
using CachingStrategies.Host.Features.Shared;
using Microsoft.Extensions.Caching.Hybrid;

namespace CachingStrategies.Host.Features.Products.WriteThroughCache;

public class WriteThroughCacheProductService(HybridCache cache, IProductRepository repository) : IProductService
{
    public async Task<Product?> GetByIdAsync(int id)
    {
        // Define the cache key
        var cacheKey = $"product:{id}";

        // Cache should always have the value but just in case we can check the database on a cache miss
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

        // Write the product to the cache (write through)
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

        // Write the updated product to the cache (write through)
        var cacheKey = $"product:{product.Id}";
        
        await cache.SetAsync(cacheKey, product, new HybridCacheEntryOptions
        {
            Expiration = TimeSpan.FromMinutes(10)
        });
    }

    public async Task DeleteAsync(int id)
    {
        // Delete the product from the database
        await repository.DeleteAsync(id);

        // Invalidate the product in the cache
        var cacheKey = $"product:{id}";
        await cache.RemoveAsync(cacheKey);
    }
}