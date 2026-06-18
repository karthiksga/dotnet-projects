namespace SidecarAPI.Models
{
    public record LogMessage
    {
        public required string Id { get; init; }
        public required DateTime Timestamp { get; init; }
        public required string Message { get; init; }
    }
}
