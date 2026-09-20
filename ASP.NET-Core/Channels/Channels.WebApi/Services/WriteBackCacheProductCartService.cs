using System.Threading.Channels;
using Channels.WebApi.Models;
using Channels.WebApi.Repositories;
using Microsoft.Extensions.Caching.Hybrid;

namespace Channels.WebApi.Services;

public class WriteBackCacheProductCartService
{
    private readonly HybridCache _cache;
    private readonly IProductCartRepository _repository;
    private readonly Channel<ProductCartDispatchEvent> _channel;

    public WriteBackCacheProductCartService(
        HybridCache cache,
        IProductCartRepository repository,
        Channel<ProductCartDispatchEvent> channel)
    {
        _cache = cache;
        _repository = repository;
        _channel = channel;
    }

    public async Task<ProductCartResponse> AddAsync(ProductCartRequest request)
    {
        var productCart = new ProductCart
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            CartItems = request.ProductCartItems.Select(x => new CartItem
            {
                Id = Guid.NewGuid(),
                Quantity = x.Quantity,
                Price = Random.Shared.Next(100, 1000)
            }).ToList()
        };

        // TODO: wrong ID here, repos has another id
        var cacheKey = $"productCart:{productCart.Id}";

        var productCartResponse = MapToProductCartResponse(productCart);
        await _cache.SetAsync(cacheKey, productCartResponse);

        await _channel.Writer.WriteAsync(new ProductCartDispatchEvent(productCart));

        return productCartResponse;
    }

    public async Task<ProductCartResponse?> GetAsync(Guid id)
    {
        var cacheKey = $"productCart:{id}";

        var cart = await _cache.GetOrCreateAsync<ProductCart?>(
	        cacheKey,
	        async token => await _repository.GetByIdAsync(id),
	        new HybridCacheEntryOptions
	        {
		        Expiration = TimeSpan.FromMinutes(10)
	        });

        if (cart is null)
        {
	        return null;
        }

        var response = MapToProductCartResponse(cart);
        return response;
    }

    public async Task DeleteAsync(Guid id)
    {
        var cacheKey = $"productCart:{id}";

        await _cache.RemoveAsync(cacheKey);

        var cart = new ProductCart { Id = id };
        await _channel.Writer.WriteAsync(new ProductCartDispatchEvent(cart));
    }

    private static ProductCartResponse MapToProductCartResponse(ProductCart cart)
    {
        return new ProductCartResponse(
            cart.Id,
            cart.UserId,
            cart.CartItems.Select(item => new CartItemResponse(
                item.Id,
                item.Id,
                item.Quantity,
                new ProductResponse(
	                item.Id,
	                item.Name,
	                item.Price)
	        )).ToList()
        );
    }
}
