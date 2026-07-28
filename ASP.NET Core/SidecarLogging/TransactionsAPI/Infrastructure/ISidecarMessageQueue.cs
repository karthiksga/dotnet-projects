namespace TransactionsAPI.Infrastructure;

public interface ISidecarMessageQueue
{
    int Count { get; }
    Task Enqueue(string level, string message);
    Task<string> Dequeue();
    Task ClearAsync();
}
