using CachingStrategies.Database.Entities;
using CachingStrategies.Domain.Products;
using CachingStrategies.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CachingStrategies.Infrastructure.Repositories;

public class ProductRepository(ApplicationDbContext context) : IProductRepository
{
    public async Task<Product?> GetByIdAsync(int id)
    {
        return await context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Product product)
    {
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        context.Products.Remove(new Product { Id = id });
        await context.SaveChangesAsync();
    }
}