using CachingStrategies.Database.Entities;

namespace CachingStrategies.Domain.Products;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
    
    Task AddAsync(Product product);
    
    Task UpdateAsync(Product product);
    
    Task DeleteAsync(int id);
}