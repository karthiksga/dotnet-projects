using System.Threading.Channels;
using Channels.WebApi.Models;
using Channels.WebApi.Repositories;

namespace Channels.WebApi.Services;

public record ProductCartDispatchEvent(ProductCart ProductCart);

public class WriteBackCacheBackgroundService(IServiceScopeFactory scopeFactory,
    Channel<ProductCartDispatchEvent> channel) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var command in channel.Reader.ReadAllAsync(stoppingToken))
        {
            using var scope = scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IProductCartRepository>();

            var existingCart = await repository.GetByIdAsync(command.ProductCart.Id);
            if (existingCart is null)
            {
                await repository.AddAsync(command.ProductCart);
                continue;
            }

            existingCart.CartItems = command.ProductCart.CartItems;
            await repository.UpdateAsync(existingCart);
        }
    }
}
