using CachingStrategies.Database.Entities;

namespace CachingStrategies.Host.Features.Shared;

public interface IProductService
{
    Task<Product?> GetByIdAsync(int id);
    
    Task AddAsync(Product product);
    
    Task UpdateAsync(Product product);
    
    Task DeleteAsync(int id);
}