

using Microsoft.EntityFrameworkCore;
using ProductGrpc.Models;

namespace ProductGrpc.Data
{
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }
        
            public DbSet<Product> Products => Set<Models.Product>();
        }
}