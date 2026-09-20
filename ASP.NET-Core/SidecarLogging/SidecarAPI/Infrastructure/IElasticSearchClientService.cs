using SidecarAPI.Models;

namespace SidecarAPI.Infrastructure;

public interface IElasticSearchClientService
{
    Task IndexAsync(LogMessage logMessage, CancellationToken ct);
    Task IndexBatchAsync(List<LogMessage> entries, CancellationToken ct);
    Task<List<LogMessage>> GetAllLogsAsync();
    Task DeleteAsyncRequest();
}
