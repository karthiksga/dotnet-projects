using CachingStrategies.Database.Entities;
using CachingStrategies.Domain.Products;
using CachingStrategies.Domain.Users;
using CachingStrategies.Infrastructure.Database.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CachingStrategies.Infrastructure.Database;

public class ApplicationDbContext(
	DbContextOptions<ApplicationDbContext> options)
	: DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCart> ProductCarts { get; set; }
    
    public DbSet<ProductCartItem> ProductCartItems { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(DatabaseConsts.Schema);
        
        modelBuilder.UseIdentityByDefaultColumns();

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new ProductCartConfiguration());
        modelBuilder.ApplyConfiguration(new ProductCartItemConfiguration());
    }
}
