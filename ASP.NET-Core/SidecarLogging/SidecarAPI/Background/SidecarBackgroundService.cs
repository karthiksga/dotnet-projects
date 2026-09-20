using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SidecarAPI.Infrastructure;
using SidecarAPI.Models;
using System.Collections.Concurrent;

namespace SidecarAPI.Background;

public class SidecarBackgroundService: BackgroundService
{
    private readonly TimeSpan _period = TimeSpan.FromSeconds(5);
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<SidecarBackgroundService> _logger;
    private readonly IOptions<SidecarSettings> _settings;

    private readonly ConcurrentQueue<string> logs = new ConcurrentQueue<string>();

    private readonly int _maxBatchSize;
    private readonly int _maxCacheDurationInMinutes;
    private readonly IMemoryCache _cache;

    public SidecarBackgroundService(
        ILogger<SidecarBackgroundService> logger, IServiceProvider serviceProvider,
        IOptions<SidecarSettings> settings, IMemoryCache cache)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _settings = settings;

        _maxBatchSize = settings.Value.MaxBatchSize;
        _maxCacheDurationInMinutes =  settings.Value.MaxCacheDurationInMinutes;
        _cache = cache;


    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(_period);
        _logger.LogInformation($"LogShipper started. Monitoring {_settings.Value.LogDirectory}");

        while (!stoppingToken.IsCancellationRequested &&
            await timer.WaitForNextTickAsync(stoppingToken))
        {
            await SendMessagesToElasticAsync(stoppingToken);
        }
    }
    private async Task SendMessagesToElasticAsync(CancellationToken cancellationToken)
    {
        var directory = _settings.Value.LogDirectory;
        var logFilePattern = _settings.Value.LogFilePattern;



        if (string.IsNullOrWhiteSpace(directory) || string.IsNullOrWhiteSpace(logFilePattern)) return;
        if (!Directory.Exists(directory)) return;

        var files = Directory.GetFiles(directory, logFilePattern);

        foreach (var fileName in files)
        {
            await using var stream = new FileStream(fileName, FileMode.Open,
                FileAccess.Read, FileShare.ReadWrite);
            using var reader = new StreamReader(stream);

            string? text;

            using IServiceScope scope = _serviceProvider.CreateScope();
            var _elasticSearchClient = scope.ServiceProvider.GetRequiredService<IElasticSearchClientService>();

            while ((text = await reader.ReadLineAsync(cancellationToken)) != null)
            {
                if (string.IsNullOrWhiteSpace(text))
                    continue;




                string[] message = text.Split('|');
                string messageKey = message[0].Trim();


                if (!_cache.TryGetValue(messageKey, out _))
                {

                    logs.Enqueue(text);

                    if (logs.Count > _maxBatchSize)
                    {
                        while (logs.TryDequeue(out string? str))
                        {
                            string[] data = str.Split('|');
                            string key = data[0].Trim();
                            if(key != messageKey)
                            {
                                _cache.Remove(key);
                            }

                            LogMessage logMessage = new LogMessage()
                            {
                                Id = data[0].Trim(),
                                Timestamp = DateTime.UtcNow,
                                Message = str.Substring(data[0].Length + 1).Trim()
                            };

                            await _elasticSearchClient.IndexAsync
                                (logMessage, cancellationToken);
                            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(
                            TimeSpan.FromMinutes(_maxCacheDurationInMinutes));
                            _cache.Set(messageKey, true, cacheEntryOptions);

                        }
                    }
                }
            }
        }
    }
}
