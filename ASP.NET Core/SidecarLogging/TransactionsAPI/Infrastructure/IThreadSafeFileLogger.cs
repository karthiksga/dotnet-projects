namespace TransactionsAPI.Infrastructure
{
    public interface IThreadSafeFileLogger
    {
        Task SendMessageAsync(string message);
        Task SendMessageAsync(string level, string message);
    }
}
