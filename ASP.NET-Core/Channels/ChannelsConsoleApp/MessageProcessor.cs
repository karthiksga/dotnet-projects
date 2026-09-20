using System.Threading.Channels;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ChannelsConsoleApp;

public class MessageProcessor : BackgroundService
{
	private readonly Channel<string> _channel;
	private readonly ILogger<MessageProcessor> _logger;

	public MessageProcessor(Channel<string> channel, ILogger<MessageProcessor> logger)
	{
		_channel = channel;
		_logger = logger;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		_logger.LogInformation("Message processor starting");

		await AddSampleMessagesAsync(stoppingToken);

		await foreach (var message in _channel.Reader.ReadAllAsync(stoppingToken))
		{
			_logger.LogInformation("Processing message: {Message}", message);

			await Task.Delay(100, stoppingToken);

			_logger.LogInformation("Message processed successfully: {Message}", message);
		}
	}

	private Task AddSampleMessagesAsync(CancellationToken stoppingToken)
	{
		_ = Task.Run(async () =>
		{
			// Add 150 messages (more than channel capacity of 100)
			for (var i = 1; i <= 150 && !stoppingToken.IsCancellationRequested; i++)
			{
				var message = $"Sample message #{i}";

				// Wait until there's space in the channel (this will block when channel is full)
				await _channel.Writer.WriteAsync(message, stoppingToken);

				_logger.LogInformation("Added message to channel: {Message}", message);

				await Task.Delay(50, stoppingToken);
			}

			// Complete the channel when done adding messages
			_channel.Writer.Complete();
		}, stoppingToken);
		return Task.CompletedTask;
	}
}
