using CachingStrategies.Database.Entities;
using CachingStrategies.Host.Features.Products.ReadThroughCache;
using Carter;

namespace CachingStrategies.Host.Features.Products.WriteThroughCache;

public record ProductRequest(string Name, decimal Price);

public record ProductResponse(int Id, string Name, decimal Price);

public class ProductsEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/write-through-cache/products/{id:int}", async (int id, WriteThroughCacheProductService productService) =>
        {
            var product = await productService.GetByIdAsync(id);
            if (product is null)
            {
                return Results.NotFound($"Product with ID {id} not found.");
            }
            
            var response = new ProductResponse(product.Id, product.Name, product.Price);
            return Results.Ok(response);
        });
        
        app.MapPost("/write-through-cache/products", async (ProductRequest request, WriteThroughCacheProductService productService) =>
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price
            };
            
            await productService.AddAsync(product);
            return Results.Created($"/products/{product.Id}", product);
        });

        app.MapPut("/write-through-cache/products/{id:int}", async (int id, ProductRequest request, WriteThroughCacheProductService productService) =>
        {
            var existingProduct = await productService.GetByIdAsync(id);
            if (existingProduct is null)
            {
                return Results.NotFound($"Product with ID {id} not found.");
            }

            existingProduct.Name = request.Name;
            existingProduct.Price = request.Price;
            
            await productService.UpdateAsync(existingProduct);
            return Results.NoContent();
        });

        app.MapDelete("/write-through-cache/products/{id:int}", async (int id, ReadThroughCacheProductService productService) =>
        {
            await productService.DeleteAsync(id);
            return Results.NoContent();
        });
    }
}