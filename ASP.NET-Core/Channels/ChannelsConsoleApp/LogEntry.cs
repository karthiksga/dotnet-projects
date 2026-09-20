namespace ChannelsConsoleApp;

public record LogEntry(string Message, LogLevel Level, DateTimeOffset Timestamp);
