using System;
using System.Collections.Generic;
using System.Text;

namespace Diagnostics.Job;

using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Runtime.Intrinsics.Arm;

[EventSource(Name = "MyCompany-App-Metrics")]
public sealed class AppMetricsEventSource : EventSource
{
    public static readonly AppMetricsEventSource Log = new();

    private PollingCounter? _cacheItemCounter;
    private PollingCounter? _processWorkingSetCounter;

    // Simulated external state or stateful service
    public static long CurrentCacheItemCount { get; set; } = 42;

    private AppMetricsEventSource() : base(EventSourceSettings.EtwSelfDescribingEventFormat)
    {
        // 1. PollingCounter with a lambda returning dynamic application state
        _cacheItemCounter = new PollingCounter(
            name: "active-cache-items",
            eventSource: this,
            metricProvider: () => CurrentCacheItemCount)
        {
            DisplayName = "Active Cache Items",
            DisplayUnits = "items"
        };

        // 2. PollingCounter querying external environment/process state
        _processWorkingSetCounter = new PollingCounter(
            name: "working-set-mb",
            eventSource: this,
            metricProvider: () => Environment.WorkingSet / (1024.0 * 1024.0))
            //Environment.WorkingSet
            //A built -in .NET property that queries the operating system for the current physical RAM footprint mapped to the process.
            //Because Environment.WorkingSet returns bytes, the number would look like 52,428,800(which is hard to read in a live monitoring dashboard).
            //Using floating-point literals(1024.0 instead of 1024) forces floating-point division rather than integer division, preserving fractional decimal values(e.g., reporting 50.25 MB instead of truncating down to 50 MB).
        {
            DisplayName = "Working Set",
            DisplayUnits = "MB"
        };
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _cacheItemCounter?.Dispose();
            _cacheItemCounter = null;

            _processWorkingSetCounter?.Dispose();
            _processWorkingSetCounter = null;
        }

        base.Dispose(disposing);
    }
}
