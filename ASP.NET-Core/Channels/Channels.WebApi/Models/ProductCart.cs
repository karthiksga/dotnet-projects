namespace Channels.WebApi.Models;

public class ProductCart
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public List<CartItem> CartItems { get; set; } = new();
}

public class CartItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public record ProductCartRequest(string UserId, List<ProductCartItemRequest> ProductCartItems);

public record ProductCartItemRequest(Guid ProductId, int Quantity);

public record ProductCartResponse(Guid Id, string UserId, List<CartItemResponse> CartItems);

public record CartItemResponse(Guid Id, Guid ProductId, int Quantity, ProductResponse? Product);

public record ProductResponse(Guid Id, string Name, decimal Price);
