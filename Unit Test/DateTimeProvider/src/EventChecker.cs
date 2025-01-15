using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeTest;
public class EventChecker
{
    private readonly IDateTimeProvider _dateTimeProvider;
    public EventChecker(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }
    public bool CanScheduleEvent()
    {
        return _dateTimeProvider.Now.DayOfWeek == DayOfWeek.Saturday ||
        _dateTimeProvider.Now.DayOfWeek == DayOfWeek.Sunday;
    }
}
