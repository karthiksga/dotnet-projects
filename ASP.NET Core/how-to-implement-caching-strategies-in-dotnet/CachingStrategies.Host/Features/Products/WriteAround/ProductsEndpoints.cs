using CachingStrategies.Database.Entities;
using CachingStrategies.Host.Features.Products.ReadThroughCache;
using Carter;

namespace CachingStrategies.Host.Features.Products.WriteAround;

public record ProductRequest(string Name, decimal Price);

public record ProductResponse(int Id, string Name, decimal Price);

public class ProductsEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/write-around/products/{id:int}", async (int id, WriteAroundCacheProductService productService) =>
        {
            var product = await productService.GetByIdAsync(id);
            if (product is null)
            {
                return Results.NotFound($"Product with ID {id} not found.");
            }
            
            var response = new ProductResponse(product.Id, product.Name, product.Price);
            return Results.Ok(response);
        });
        
        app.MapPost("/write-around/products", async (ProductRequest request, WriteAroundCacheProductService productService) =>
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price
            };
            
            await productService.AddAsync(product);
            return Results.Created($"/products/{product.Id}", product);
        });

        app.MapPut("/write-around/products/{id:int}", async (int id, ProductRequest request, WriteAroundCacheProductService productService) =>
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

        app.MapDelete("/write-around/products/{id:int}", async (int id, ReadThroughCacheProductService productService) =>
        {
            await productService.DeleteAsync(id);
            return Results.NoContent();
        });
    }
}