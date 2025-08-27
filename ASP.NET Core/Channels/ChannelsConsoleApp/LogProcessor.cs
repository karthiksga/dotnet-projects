using System.Threading.Channels;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ChannelsConsoleApp;

public class LogProcessor : BackgroundService
{
	private readonly Channel<LogEntry> _channel;
	private readonly ILogger<LogProcessor> _logger;

	public LogProcessor(Channel<LogEntry> channel, ILogger<LogProcessor> logger)
	{
		_channel = channel;
		_logger = logger;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		_logger.LogInformation("Log processor starting");

		await AddSampleLogsAsync(stoppingToken);

		await foreach (var logEntry in _channel.Reader.ReadAllAsync(stoppingToken))
		{
			var msLogLevel = ToMicrosoftLogLevel(logEntry);
			LogMessageWithLevel(msLogLevel, logEntry);

			await Task.Delay(50, stoppingToken);
		}
	}

	private void LogMessageWithLevel(Microsoft.Extensions.Logging.LogLevel msLogLevel, LogEntry logEntry)
	{
		_logger.Log(
			msLogLevel,
			"[{Timestamp}] {Level}: {Message}",
			logEntry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff"),
			logEntry.Level,
			logEntry.Message);
	}

	private static Microsoft.Extensions.Logging.LogLevel ToMicrosoftLogLevel(LogEntry logEntry)
	{
		return logEntry.Level switch
		{
			LogLevel.Debug => Microsoft.Extensions.Logging.LogLevel.Debug,
			LogLevel.Information => Microsoft.Extensions.Logging.LogLevel.Information,
			LogLevel.Warning => Microsoft.Extensions.Logging.LogLevel.Warning,
			LogLevel.Error => Microsoft.Extensions.Logging.LogLevel.Error,
			LogLevel.Critical => Microsoft.Extensions.Logging.LogLevel.Critical,
			_ => Microsoft.Extensions.Logging.LogLevel.Information
		};
	}

	private Task AddSampleLogsAsync(CancellationToken stoppingToken)
	{
		_ = Task.Run(async () =>
		{
			var logEntries = new[]
			{
				new LogEntry("Application started", LogLevel.Information, DateTimeOffset.UtcNow),
				new LogEntry("Debug information", LogLevel.Debug, DateTimeOffset.UtcNow.AddSeconds(1)),
				new LogEntry("Warning: CPU usage high", LogLevel.Warning, DateTimeOffset.UtcNow.AddSeconds(2)),
				new LogEntry("Error: Failed to connect to database", LogLevel.Error, DateTimeOffset.UtcNow.AddSeconds(3)),
				new LogEntry("Critical: System shutdown imminent", LogLevel.Critical, DateTimeOffset.UtcNow.AddSeconds(4))
			};

			foreach (var entry in logEntries)
			{
				if (stoppingToken.IsCancellationRequested)
				{
					break;
				}

				await _channel.Writer.WriteAsync(entry, stoppingToken);

				await Task.Delay(500, stoppingToken);
			}

			// In a real application, we wouldn't complete the channel like this
			// since logs would keep coming. But for the demo, we'll complete it.

			await Task.Delay(5000, stoppingToken);
			_channel.Writer.Complete();

		}, stoppingToken);
		return Task.CompletedTask;
	}
}
