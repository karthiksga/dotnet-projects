using System.Collections.Concurrent;
using Channels.WebApi.Models;

namespace Channels.WebApi.Repositories;

public class InMemoryProductCartRepository : IProductCartRepository
{
    private readonly ConcurrentDictionary<Guid, ProductCart> _carts = new();

    public Task<ProductCart?> GetByIdAsync(Guid id)
    {
        _carts.TryGetValue(id, out var cart);
        return Task.FromResult(cart);
    }

    public Task<List<ProductCart>> GetAllAsync()
    {
        return Task.FromResult(_carts.Values.ToList());
    }

    public Task AddAsync(ProductCart productCart)
    {
        _carts[productCart.Id] = productCart;
        return Task.CompletedTask;
    }

    public Task UpdateAsync(ProductCart productCart)
    {
        _carts[productCart.Id] = productCart;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        _carts.TryRemove(id, out _);
        return Task.CompletedTask;
    }
}
