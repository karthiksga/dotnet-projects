
namespace ProductGrpc.Models
{
    public class Product
    {
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public string?  Tags { get; set; }  
        
    }
}