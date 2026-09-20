using System.Collections.Concurrent;

namespace TransactionsAPI.Infrastructure
{
    public sealed class SidecarMessageQueue : ISidecarMessageQueue
    {
        private readonly ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
        public async Task Enqueue(string level, string message)
        {
            string str = await BuildMessage(level, message);
            queue.Enqueue(str);
        }
        public async Task<string> Dequeue()
        {
            if (queue.TryDequeue(out string? message))
            {
                return message;
            }

            return string.Empty;
        }
        private async Task<string> BuildMessage(string level, string message)
        {
            return $"{level} | {message}{Environment.NewLine}";
        }
        public int Count => queue.Count;

        public async Task ClearAsync()
        {
            while (queue.TryDequeue(out _)) { }
        }
    }
}
