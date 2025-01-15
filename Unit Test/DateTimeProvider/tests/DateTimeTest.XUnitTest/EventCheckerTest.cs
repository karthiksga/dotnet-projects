using Xunit;

namespace DateTimeTest.Test;
public class EventCheckerTest
{
    [Fact]
    public void CanScheduleEvent_returns_true_when_day_is_sunday()
    {
        var sunday = new DateTime(2024, 2, 4);
        var fakeDateTimeProvider = new FakeDateTimeProvider(sunday);
        var eventChecker = new EventChecker(fakeDateTimeProvider);

        var result = eventChecker.CanScheduleEvent();

        Assert.True(result);
    }
}