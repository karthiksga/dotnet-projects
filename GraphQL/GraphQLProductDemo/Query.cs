using GraphQLProductDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQLProductDemo
{
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Product> GetProducts([Service] IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            var context = dbContextFactory.CreateDbContext();
            return context.Products;
        }
    }
}
