using System.Threading.Channels;
using ChannelsConsoleApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var channel = Channel.CreateUnbounded<int>();

// Producer
_ = Task.Run(async () =>
{
	for (var i = 0; i < 10; i++)
	{
		await channel.Writer.WriteAsync(i);
		Console.WriteLine($"Produced: {i}");
		await Task.Delay(100); // simulate work
	}
	channel.Writer.Complete();
});

// Consumer
await foreach (var item in channel.Reader.ReadAllAsync())
{
	Console.WriteLine($"Consumed: {item}");
	await Task.Delay(150); // simulate processing
}

Console.WriteLine("Processing complete.");




var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton(_ => Channel.CreateBounded<string>(new BoundedChannelOptions(100)
{
	FullMode = BoundedChannelFullMode.Wait
}));

builder.Services.AddSingleton(_ => Channel.CreateUnbounded<LogEntry>(new UnboundedChannelOptions
{
	SingleReader = true,
	SingleWriter = false
}));

builder.Services.AddHostedService<MessageProcessor>();
builder.Services.AddHostedService<LogProcessor>();

var host = builder.Build();
await host.RunAsync();
