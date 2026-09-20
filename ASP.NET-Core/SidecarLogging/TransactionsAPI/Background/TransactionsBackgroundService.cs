using TransactionsAPI.Infrastructure;

namespace TransactionsAPI.Background;

public class TransactionsBackgroundService: BackgroundService
{
    private readonly TimeSpan _period = TimeSpan.FromSeconds(5);
    private readonly ILogger<TransactionsBackgroundService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public TransactionsBackgroundService(ILogger<TransactionsBackgroundService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(_period);
        using IServiceScope scope = _serviceProvider.CreateScope();

        var _transactionsMessageQueue = scope.ServiceProvider.GetRequiredService<ISidecarMessageQueue>();
        var threadSafeFileLogger = scope.ServiceProvider.GetRequiredService<IThreadSafeFileLogger>();

        while (!stoppingToken.IsCancellationRequested &&
                  await timer.WaitForNextTickAsync(stoppingToken))
        {
            _logger.LogInformation("Executing PeriodicBackgroundTask");

            while (_transactionsMessageQueue.Count > 0)
            {
                string message = await _transactionsMessageQueue.Dequeue();
                await threadSafeFileLogger.SendMessageAsync(message);
            }
        }
    }
}
