# Channels Example Application

This application demonstrates how to use `System.Threading.Channels` in .NET to create efficient producer-consumer scenarios.

## Features

### Bounded Channels

Bounded channels have a fixed capacity and can be configured to handle full channels in different ways:

- **Wait Mode**: When the channel is full, `WriteAsync` will wait until space becomes available
- **Drop Mode**: When the channel is full, new messages will be dropped
- **DropOldest Mode**: When the channel is full, the oldest message will be dropped to make room for the new one
- **DropNewest Mode**: When the channel is full, the newest message will be dropped

Bounded channels are useful when:
- You need to implement backpressure
- You want to limit memory usage
- Producers might outpace consumers

### Unbounded Channels

Unbounded channels don't have a capacity limit and will continue to accept messages as long as memory is available.

Unbounded channels are useful when:
- Consumers can keep up with producers
- Messages must never be dropped
- Memory usage isn't a concern

## Channel Options

Both channel types support these options:

- **SingleReader**: Optimize for a single reader
- **SingleWriter**: Optimize for a single writer
- **AllowSynchronousContinuations**: Allow callbacks to execute synchronously

## Usage Examples

This sample application includes:

1. **MessageProcessor**: Uses a bounded channel with a capacity of 100 messages
2. **LogProcessor**: Uses an unbounded channel for logging
3. **ChannelExtensions**: Utility methods for working with channels

## Best Practices

- Use bounded channels when you need backpressure
- Use unbounded channels for logging or when dropping messages is unacceptable
- Complete the writer when no more items will be written
- Handle ChannelClosedException when reading
- Use cancellation tokens for clean shutdown
