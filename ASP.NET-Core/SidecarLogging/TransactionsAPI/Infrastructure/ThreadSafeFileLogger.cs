namespace TransactionsAPI.Infrastructure;

public class ThreadSafeFileLogger:IThreadSafeFileLogger
{
    private static readonly SemaphoreSlim _semaphore = new(1, 1);
    private readonly IConfiguration _configuration;
    private readonly string _filePath;

    public ThreadSafeFileLogger(IConfiguration configuration)
    {
        _configuration = configuration;

        _filePath = _configuration["ApiKeys:FilePath"] ??
            throw new InvalidOperationException("Path to file missing ...");
    }

    public async Task SendMessageAsync(string message)
    {
        await _semaphore.WaitAsync();
        try
        {
            await File.AppendAllTextAsync(_filePath,
                $"{Guid.NewGuid().ToString()} | {message}{Environment.NewLine}");
        }
        finally
        {
            _semaphore.Release();
        }
    }
    public async Task SendMessageAsync(string level, string message)
    {
        await _semaphore.WaitAsync();
        try
        {
            await File.AppendAllTextAsync(_filePath,
                $"{Guid.NewGuid().ToString()} | {level} | {message}{Environment.NewLine}");
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
