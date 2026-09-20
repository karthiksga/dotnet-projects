namespace DiagnosticScenarios.Infrastructure.Tracing;

using System.Diagnostics.Tracing;

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