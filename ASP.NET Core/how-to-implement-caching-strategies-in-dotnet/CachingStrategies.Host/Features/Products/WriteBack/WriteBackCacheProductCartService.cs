using System.Threading.Channels;
using CachingStrategies.Domain.Products;
using CachingStrategies.Host.Features.Products.WriteBack.Models;
using Microsoft.Extensions.Caching.Hybrid;

namespace CachingStrategies.Host.Features.Products.WriteBack;

public class WriteBackCacheProductCartService(
    HybridCache cache,
    IProductCartRepository repository,
    IProductRepository productRepository,
    Channel<ProductCartDispatchEvent> channel)
{
    public async Task<ProductCartResponse?> GetByIdAsync(Guid id)
    {
        // Define the cache key
        var cacheKey = $"productCart:{id}";

        // Retrieve the ProductCart from the cache (or from the database if missing)
        var productCartResponse = await cache.GetOrCreateAsync<ProductCartResponse?>(
            cacheKey,
            // Data loader function to fetch from DB
            async token => 
            {
                var productCart = await repository.GetByIdAsync(id);
                return productCart?.MapToProductCartResponse();
            },
            new HybridCacheEntryOptions
            {
                Expiration = TimeSpan.FromMinutes(10)
            });

        return productCartResponse;
    }

    public async Task<ProductCartResponse> AddAsync(ProductCartRequest request)
    {
        var productCart = new ProductCart
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            CartItems = request.ProductCartItems.Select(x => x.MapToCartItem()).ToList()
        };
        
        foreach (var cartItem in productCart.CartItems)
        {
            var product = await productRepository.GetByIdAsync(cartItem.ProductId);
            if (product is not null)
            {
                cartItem.Product = product;
            }
        }
        
        var cacheKey = $"productCart:{productCart.Id}";

        // Add or update the ProductCart in the cache
        var productCartResponse = productCart.MapToProductCartResponse();
        await cache.SetAsync(cacheKey, productCartResponse);

        // Queue the ProductCart for database syncing
        productCart.CartItems.ForEach(x => x.Product = null!);
        
        await channel.Writer.WriteAsync(new ProductCartDispatchEvent(productCart, false));

        return productCartResponse;
    }
    
    public async Task<ProductCartResponse?> UpdateAsync(Guid cartId, ProductCartRequest request)
    {
        var cacheKey = $"productCart:{cartId}";
        var productCartResponse = await GetByIdAsync(cartId);
        if (productCartResponse is null)
        {
            return null;
        }

        productCartResponse = productCartResponse with
        {
            ProductCartItems = GetUpdatedCartItems(productCartResponse.ProductCartItems, request.ProductCartItems).ToList()
        };
        
        await cache.SetAsync(cacheKey, productCartResponse);

        // Queue the ProductCart for database syncing
        var productCart = productCartResponse.MapToProductCart();
        await channel.Writer.WriteAsync(new ProductCartDispatchEvent(productCart, false));

        return productCartResponse;
    }
    
    public async Task DeleteByIdAsync(Guid id)
    {
        // Define the cache key
        var cacheKey = $"productCart:{id}";

        // Remove from the cache
        await cache.RemoveAsync(cacheKey);

        // Queue a deletion operation
        var dispatchCommand = new ProductCartDispatchEvent(new ProductCart { Id = id }, true);
        await channel.Writer.WriteAsync(dispatchCommand);
    }
    
    private IEnumerable<ProductCartItemResponse> GetUpdatedCartItems(List<ProductCartItemResponse> existingItems, List<ProductCartItemRequest> requestItems)
    {
        foreach (var itemResponse in existingItems)
        {
            var requestItem = requestItems.FirstOrDefault(x => x.ProductId == itemResponse.ProductId);
            if (requestItem is not null)
            {
                yield return itemResponse with { Quantity = requestItem.Quantity };
            }
        }
    }
}