using Grpc.Core;
using ProductGrpc.Data;
using ProductGrpc.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductGrpc.Services
{
    public class ProductService : ProductsServiceProto.ProductsServiceProtoBase
    {
        private readonly AppDbContext _dbContext;

        public ProductService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<CreateProductResponse> CreateProduct(CreateProductRequest request,
           ServerCallContext context)
        {
            try
            {
                var productItem = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Description = request.Description,
                    Price = Convert.ToDecimal(request.Price),
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                    Tags = request.Tag
                };

                _dbContext.Products.Add(productItem);
                await _dbContext.SaveChangesAsync();

                return new CreateProductResponse
                {
                    Success = true,
                    Message = "Product successfully created",
                    Product = MapToProductModel(productItem)
                };
            }
            catch (Exception ex)
            {
                return new CreateProductResponse
                {
                    Success = false,
                    Message = $"Error creating product: {ex.Message}"
                };
            }
        }

        public override async Task<GetProductResponse> GetProduct(GetProductRequest request, ServerCallContext context)
        {
            try
            {
                if (!Guid.TryParse(request.Id, out var productId))
                {
                    return new GetProductResponse
                    {
                        Success = false,
                        Message = "Invalid product ID format"
                    };
                }

                var product = await _dbContext.Products.FindAsync(productId);

                if (product == null)
                {
                    return new GetProductResponse
                    {
                        Success = false,
                        Message = "Product not found"
                    };
                }

                return new GetProductResponse
                {
                    Success = true,
                    Message = "Product retrieved successfully",
                    Product = MapToProductModel(product)
                };
            }
            catch (Exception ex)
            {
                return new GetProductResponse
                {
                    Success = false,
                    Message = $"Error retrieving product: {ex.Message}"
                };
            }
        }

        public override async Task<ListProductsResponse> ListProduct(ListProductsRequest request, ServerCallContext context)
        {
            try
            {
                var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;
                var page = request.Page <= 0 ? 1 : request.Page;

                // Get total Count for pagination 
                var totalCount = await _dbContext.Products.CountAsync();
                var productItemList = await _dbContext.Products
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var response = new ListProductsResponse
                {
                    Success = true,
                    Message = productItemList.Any() ? "Products retrieved successfully" : "No products found",
                    TotalCount = totalCount
                };

                response.Products.AddRange(productItemList.Select(MapToProductModel));

                return response;
            }
            catch (Exception ex)
            {
                return new ListProductsResponse
                {
                    Success = false,
                    Message = $"Error retrieving products: {ex.Message}",
                    TotalCount = 0
                };
            }
        }


        public override async Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request,
        ServerCallContext context)
        {
            try
            {
                if (!Guid.TryParse(request.Id, out var productId))
                {
                    return new UpdateProductResponse
                    {
                        Success = false,
                        Message = "Invalid product ID format"
                    };
                }

                var existingProduct = await _dbContext.Products.FindAsync(productId);

                if (existingProduct == null)
                {
                    return new UpdateProductResponse
                    {
                        Success = false,
                        Message = "Product not found"
                    };
                }

                // Update properties
                existingProduct.Name = request.Name;
                existingProduct.Description = request.Description;
                existingProduct.Price = Convert.ToDecimal(request.Price);
                existingProduct.Tags = request.Tag;
                existingProduct.Updated = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();

                return new UpdateProductResponse
                {
                    Success = true,
                    Message = "Product updated successfully",
                    Product = MapToProductModel(existingProduct)
                };
            }
            catch (Exception ex)
            {
                return new UpdateProductResponse
                {
                    Success = false,
                    Message = $"Error updating product: {ex.Message}"
                };
            }
        }

        public override async Task<DeleteProductResponse> DeleteProduct(DeleteProductRequest request, ServerCallContext context)
        {
            try
            {
                if (!Guid.TryParse(request.Id, out var productId))
                {
                    return new DeleteProductResponse
                    {
                        Success = false,
                        Message = "Invalid product ID format"
                    };
                }

                var product = await _dbContext.Products.FindAsync(productId);

                if (product == null)
                {
                    return new DeleteProductResponse
                    {
                        Success = false,
                        Message = "Product not found"
                    };
                }

                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();

                return new DeleteProductResponse
                {
                    Success = true,
                    Message = "Product deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new DeleteProductResponse
                {
                    Success = false,
                    Message = $"Error deleting product: {ex.Message}"
                };
            }
        }

        
        private static ProductModel MapToProductModel(Product product)
        {
            return new ProductModel
            {
                Id = product.Id.ToString(),
                Name = product.Name,
                Description = product.Description,
                Price = (double)product.Price,
                CreatedAt = product.Created.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                UpdatedAt = product.Updated.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                Tag = product.Tags ?? string.Empty
            };
        }


    }
}
