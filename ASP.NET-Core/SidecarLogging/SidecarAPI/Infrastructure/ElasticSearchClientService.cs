using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using Elastic.Transport;
using Microsoft.Extensions.Options;
using SidecarAPI.Models;

namespace SidecarAPI.Infrastructure;

public class ElasticSearchClientService: IElasticSearchClientService
{
    private readonly ILogger<ElasticSearchClientService> _logger;
    private readonly ElasticsearchClient _elasticSearchClient;
    private readonly ElasticsearchClientSettings _elasticSearchClientSettings;
    public ElasticSearchClientService(
        ILogger<ElasticSearchClientService> logger,
        IOptions<SidecarSettings> settings)
    {
        _logger = logger;

        //_elasticSearchClientSettings = new ElasticsearchClientSettings(
        //    new Uri(settings.Value.Elasticsearch.Url))
        //    .Authentication(
        //        new BasicAuthentication(
        //            settings.Value.Elasticsearch.Username,
        //            settings.Value.Elasticsearch.Password));
        _elasticSearchClientSettings = new ElasticsearchClientSettings(
           new Uri(settings.Value.Elasticsearch.Url));

        _elasticSearchClient = new ElasticsearchClient(_elasticSearchClientSettings);
    }
    public async Task DeleteAsyncRequest()
    {
        var today = DateTime.UtcNow.ToString("yyyy.MM.dd");
        var indexName = $"sidecar-application-logs-{today}";
        var response = await _elasticSearchClient.Indices.DeleteAsync(indexName);
    }
    public async Task<List<LogMessage>> GetAllLogsAsync()
    {
        var today = DateTime.UtcNow.ToString("yyyy.MM.dd");
        var indexName = $"sidecar-application-logs-{today}";
        var searchResponse = await _elasticSearchClient.SearchAsync<LogMessage>(
            s => s.Indices(indexName).Query(q => q.MatchAll()));
        return searchResponse.IsValidResponse ? searchResponse.Documents?.ToList() ??
            new List<LogMessage>() : new List<LogMessage>();
    }

    public async Task IndexAsync(LogMessage logMessage, CancellationToken ct)
    {
        var today = DateTime.UtcNow.ToString("yyyy.MM.dd");
        var indexName = $"sidecar-application-logs-{today}";
        var existsResponse = await _elasticSearchClient.Indices.ExistsAsync(indexName, ct);

        if (!existsResponse.Exists)
        {
            var createResponse = await _elasticSearchClient.Indices.CreateAsync(indexName);

            if (!createResponse.IsValidResponse)
            {
                _logger.LogError("Failed to create index: {Error}", createResponse.DebugInformation);
                throw new Exception(createResponse.DebugInformation);
            }
        }


        var indexResponse =
            await _elasticSearchClient.IndexAsync(logMessage, idx => idx.Index(indexName));
        if (!indexResponse.IsValidResponse)
        {
            throw new Exception(indexResponse.DebugInformation);
        }

    }
    public async Task IndexBatchAsync(List<LogMessage> entries, CancellationToken ct)
    {
        if (entries.Count == 0) return;

        var today = DateTime.UtcNow.ToString("yyyy.MM.dd");
        var indexName = $"sidecar-application-logs-{today}";

        var bulkRequest = new BulkRequest(indexName)
        {
            Operations = new List<IBulkOperation>()
        };

        foreach (var entry in entries)
        {
            bulkRequest.Operations.Add(new BulkIndexOperation<LogMessage>(entry));
        }

        var response = await _elasticSearchClient.BulkAsync(bulkRequest, ct);

        if (!response.IsValidResponse)
        {
            _logger.LogError("Failed to index logs: {Error}", response.DebugInformation);
            throw new Exception($"Elasticsearch error: {response.DebugInformation}");
        }

        _logger.LogInformation("Indexed {Count} logs to {Index}", entries.Count, indexName);
    }
}
