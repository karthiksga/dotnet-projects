using Diagnostics.Job;

Console.WriteLine($"Application started. PID: {Environment.ProcessId}");
Console.WriteLine("Press Ctrl+C to exit.\n");

// Ensure static constructor / instance runs
_ = AppMetricsEventSource.Log;

var random = new Random();

while (true)
{
    /*
     * Polling Counter example: The PollingCounter will call the provided lambda to get the current cache item count every second. Here, we simulate changes in the cache item count by randomly increasing or decreasing it. The PollingCounter will report the current value to any listeners (like EventPipe or PerfView) every second.
     */
    // Simulate cache fluctuations
    AppMetricsEventSource.CurrentCacheItemCount += random.Next(-5, 10);
    if (AppMetricsEventSource.CurrentCacheItemCount < 0)
    {
        AppMetricsEventSource.CurrentCacheItemCount = 0;
    }

    /*
     * Incrementing Polling Counter example: The IncrementingPollingCounter will calculate the rate of bytes written based on the cumulative total. Here, we simulate writing a random number of bytes to storage every second. The IncrementingPollingCounter will report the rate of bytes written per second to any listeners.
     */
    // Simulate writing between 50 KB and 200 KB per operation
    var bytes = random.Next(50 * 1024, 200 * 1024);

    // Increment the cumulative total
    StorageMetricsEventSource.TotalBytesWritten += bytes;


    await Task.Delay(1000);
}