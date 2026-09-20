namespace SidecarAPI.Models;

public class SidecarSettings
{
    public string LogDirectory { get; set; }
    public string LogFilePattern { get; set; }
    public int MaxBatchSize { get; set; }
    public int MaxCacheEntries { get; set; }
    public int MaxCacheDurationInMinutes { get; set; }
    public Elasticsearch Elasticsearch { get; set; }
}

public class Elasticsearch
{
    public string Url { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
