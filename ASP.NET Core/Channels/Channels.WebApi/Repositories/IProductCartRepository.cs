using Channels.WebApi.Models;

namespace Channels.WebApi.Repositories;

public interface IProductCartRepository
{
    Task<ProductCart?> GetByIdAsync(Guid id);
    Task<List<ProductCart>> GetAllAsync();
    Task AddAsync(ProductCart productCart);
    Task UpdateAsync(ProductCart productCart);
    Task DeleteAsync(Guid id);
}
