using CachingStrategies.Domain.Products;
using CachingStrategies.Host.Features.Products.WriteBack.Models;
using Carter;

namespace CachingStrategies.Host.Features.Products.WriteBack;

public class ProductCartsEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/write-back/product-carts/{cartId:guid}", async (Guid cartId, WriteBackCacheProductCartService productCartService) =>
        {
            var productCartResponse = await productCartService.GetByIdAsync(cartId);
            if (productCartResponse is null)
            {
                return Results.NotFound($"ProductCart with ID {cartId} not found.");
            }

            return Results.Ok(productCartResponse);
        });

        app.MapPost("/write-back/product-carts", async (ProductCartRequest request, WriteBackCacheProductCartService productCartService) =>
        {
            var response = await productCartService.AddAsync(request);

            return Results.Created($"/write-back/product-carts/{response.Id}", response);
        });

        app.MapPut("/write-back/product-carts/{cartId:guid}", async (Guid cartId, ProductCartRequest request, WriteBackCacheProductCartService productCartService) =>
        {
            var existingProductCart = await productCartService.GetByIdAsync(cartId);
            if (existingProductCart is null)
            {
                return Results.NotFound($"ProductCart with ID {cartId} not found.");
            }

            if (existingProductCart.UserId != request.UserId)
            {
                return Results.BadRequest("Cannot update a ProductCart with a different UserId.");
            }

            await productCartService.UpdateAsync(cartId, request);

            return Results.NoContent();
        });

        app.MapDelete("/write-back/product-carts/{cartId:guid}", async (Guid cartId, WriteBackCacheProductCartService productCartService) =>
        {
            await productCartService.DeleteByIdAsync(cartId);
            return Results.NoContent();
        });
    }
}