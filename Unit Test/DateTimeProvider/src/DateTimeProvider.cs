using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeTest;
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
}

public interface IDateTimeProvider
{
   DateTime Now { get; }
}