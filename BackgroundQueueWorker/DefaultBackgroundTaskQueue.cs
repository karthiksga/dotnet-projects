using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace BackgroundQueueWorker;

internal class DefaultBackgroundTaskQueue : IBackgroundTaskQueue
{
    private readonly Channel<Func<CancellationToken, ValueTask>> _queue;
    public DefaultBackgroundTaskQueue(int capacity)
    {
        BoundedChannelOptions options = new(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };
        _queue=Channel.CreateBounded<Func<CancellationToken, ValueTask>>(options);
    }
    public async ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(CancellationToken cancellationToken)
    {
        Func<CancellationToken, ValueTask>? workItem = await _queue.Reader.ReadAsync(cancellationToken);
        return workItem;
    }

    public async ValueTask QueueBackgroundWorkItemAsync(Func<CancellationToken, ValueTask> workItem)
    {
        ArgumentNullException.ThrowIfNull(workItem);
        await _queue.Writer.WriteAsync(workItem);
    }
}
