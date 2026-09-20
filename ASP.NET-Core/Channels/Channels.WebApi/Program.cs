using System.Threading.Channels;
using Channels.WebApi.Repositories;
using Channels.WebApi.Services;
using Microsoft.Extensions.Caching.Hybrid;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IProductCartRepository, InMemoryProductCartRepository>();

builder.Services.AddHybridCache(options =>
{
	options.DefaultEntryOptions = new HybridCacheEntryOptions
	{
		Expiration = TimeSpan.FromMinutes(10),
		LocalCacheExpiration = TimeSpan.FromMinutes(10)
	};
});

builder.Services.AddSingleton(_ => Channel.CreateBounded<ProductCartDispatchEvent>(new BoundedChannelOptions(100)
{
    FullMode = BoundedChannelFullMode.Wait
}));

builder.Services.AddHostedService<WriteBackCacheBackgroundService>();
builder.Services.AddScoped<WriteBackCacheProductCartService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
