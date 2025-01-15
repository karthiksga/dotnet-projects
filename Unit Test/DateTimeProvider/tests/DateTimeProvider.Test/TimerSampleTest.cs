using Microsoft.Extensions.Time.Testing;
using Xunit;

namespace DateTimeTest.Test;


public class TimerSampleTest
{
    [Fact]
    public async Task TestTimer()
    {
        FakeTimeProvider fakeTimeProvider = new(DateTimeOffset.Now);

        TimerSample timerSample = new(fakeTimeProvider);

        await Task.Delay(2000);

        Assert.Equal(0, timerSample.Value);

        await timerSample.DisposeAsync();
    }
}
