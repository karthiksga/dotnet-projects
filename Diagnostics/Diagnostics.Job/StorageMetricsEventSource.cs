using System;
using System.Collections.Generic;
using System.Text;

namespace Diagnostics.Job;

using System.Diagnostics.Tracing;

[EventSource(Name = "MyCompany-Storage-Metrics")]
public sealed class StorageMetricsEventSource : EventSource
{
    public static readonly StorageMetricsEventSource Log = new();

    private IncrementingPollingCounter? _bytesWrittenRateCounter;

    // Cumulative total tracked by your service (always goes up)
    public static long TotalBytesWritten { get; set; } = 0;

    private StorageMetricsEventSource() : base(EventSourceSettings.EtwSelfDescribingEventFormat)
    {
        // IncrementingPollingCounter monitors the cumulative total
        // and reports (CurrentTotal - PreviousTotal) / Interval
        _bytesWrittenRateCounter = new IncrementingPollingCounter(
            name: "bytes-written-rate",
            eventSource: this,
            totalValueProvider: () => TotalBytesWritten)
        {
            DisplayName = "Disk Write Rate",
            DisplayRateTimeScale = TimeSpan.FromSeconds(1), // Reports per second
            DisplayUnits = "B/s"
        };
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _bytesWrittenRateCounter?.Dispose();
            _bytesWrittenRateCounter = null;
        }

        base.Dispose(disposing);
    }
}