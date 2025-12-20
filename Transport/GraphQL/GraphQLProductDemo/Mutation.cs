using GraphQLProductDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQLProductDemo
{

    public class Mutation
    {
        public async Task<Product> AddProductAsync(Product product, [Service] IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            var context = dbContextFactory.CreateDbContext();
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return product;
        }
    }
}
