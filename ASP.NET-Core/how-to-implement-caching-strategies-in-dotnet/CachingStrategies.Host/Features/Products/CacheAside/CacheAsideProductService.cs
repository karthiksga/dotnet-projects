using CachingStrategies.Database.Entities;
using CachingStrategies.Domain.Products;
using CachingStrategies.Host.Features.Shared;
using Microsoft.Extensions.Caching.Memory;

namespace CachingStrategies.Host.Features.Products.CacheAside;

public class CacheAsideProductService(IMemoryCache cache, IProductRepository repository) : IProductService
{
    public async Task<Product?> GetByIdAsync(int id)
    {
        // Define the cache key
        var cacheKey = $"product:{id}";

        // 1. Try to get the product from the cache
        var product = cache.Get<Product>(cacheKey);
        if (product is not null)
        {
            return product; // Cache hit
        }

        // 2. If not found, load the product from the database
        product = await repository.GetByIdAsync(id);
        if (product != null)
        {
            // 3. Update the cache with the fetched product
            cache.Set(cacheKey, product, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });
        }

        return product;
    }

    public async Task AddAsync(Product product)
    {
        // Add the product to the database
        await repository.AddAsync(product);

        // Update the cache with the new product
        var cacheKey = $"product:{product.Id}";
        
        cache.Set(cacheKey, product, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        });
    }

    public async Task UpdateAsync(Product product)
    {
        // Update the product in the database
        await repository.UpdateAsync(product);

        // Refresh the product in the cache
        var cacheKey = $"product:{product.Id}";
        
        cache.Set(cacheKey, product, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        });
    }

    public async Task DeleteAsync(int id)
    {
        // Remove the product from the database
        await repository.DeleteAsync(id);

        // Invalidate the cache entry
        var cacheKey = $"product:{id}";
        cache.Remove(cacheKey);
    }
}