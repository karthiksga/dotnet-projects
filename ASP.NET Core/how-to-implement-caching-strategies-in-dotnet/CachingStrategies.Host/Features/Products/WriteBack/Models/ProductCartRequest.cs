namespace CachingStrategies.Host.Features.Products.WriteBack.Models;

public record ProductCartRequest(int UserId, List<ProductCartItemRequest> ProductCartItems);