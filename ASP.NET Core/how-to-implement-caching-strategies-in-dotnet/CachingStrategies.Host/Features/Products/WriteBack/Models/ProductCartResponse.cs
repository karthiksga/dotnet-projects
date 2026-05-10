namespace CachingStrategies.Host.Features.Products.WriteBack.Models;

public record ProductCartResponse(Guid Id, int UserId, List<ProductCartItemResponse> ProductCartItems);