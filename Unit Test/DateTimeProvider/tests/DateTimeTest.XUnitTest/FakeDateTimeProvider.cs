using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeTest.Test;
public class FakeDateTimeProvider : IDateTimeProvider
{
    public DateTime Now { get; set; }
    public FakeDateTimeProvider(DateTime date)
    {
        Now = date;
    }
}
