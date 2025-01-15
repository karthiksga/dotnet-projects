using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeTest;
public class TimerSample : IAsyncDisposable
{
    private readonly TimeProvider _timeProvider;
    private readonly CancellationTokenSource _cancellationTokenSource;

    public int Value { get; set; } = 0;

    public TimerSample(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
        _cancellationTokenSource = new CancellationTokenSource();

        Task.Run(RunInTheLoop);
    }

    private async Task RunInTheLoop()
    {
        var token = _cancellationTokenSource.Token;

        while (!token.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(1), _timeProvider);
            Value++;
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _cancellationTokenSource.CancelAsync();
    }
}