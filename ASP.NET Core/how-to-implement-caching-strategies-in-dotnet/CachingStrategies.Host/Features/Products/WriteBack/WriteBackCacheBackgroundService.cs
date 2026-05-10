using System.Threading.Channels;
using CachingStrategies.Domain.Products;

namespace CachingStrategies.Host.Features.Products.WriteBack;

public record ProductCartDispatchEvent(ProductCart ProductCart, bool IsDeleted);

public class WriteBackCacheBackgroundService(IServiceScopeFactory scopeFactory,
    Channel<ProductCartDispatchEvent> channel) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var command in channel.Reader.ReadAllAsync(stoppingToken))
        {
            using var scope = scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IProductCartRepository>();
            
            if (command.IsDeleted)
            {
                await repository.DeleteAsync(command.ProductCart.Id);
                return;
            }

            var existingCart = await repository.GetByIdAsync(command.ProductCart.Id);
            if (existingCart is null)
            {
                await repository.AddAsync(command.ProductCart);
            }
            else
            {
                existingCart.CartItems = command.ProductCart.CartItems;
                await repository.UpdateAsync(existingCart);
            }
        }
    }
}