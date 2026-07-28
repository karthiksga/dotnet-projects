using System.Text;
using DotNetEnv;
using RabbitMQ.Client;

Console.WriteLine("=== RabbitMQ Producer (v7 Async) ===");
Console.WriteLine("Type a message and press ENTER. Empty line quits.\n");

// Load .env first
Env.Load();

// Connection setup
var factory = new ConnectionFactory
{
    HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost",
    Port = int.TryParse(Environment.GetEnvironmentVariable("RABBITMQ_PORT"), out var port) ? port : 5672,
    UserName = Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? "guest",
    Password = Environment.GetEnvironmentVariable("RABBITMQ_PASS") ?? "guest"
};

await using var connection = await factory.CreateConnectionAsync();

// Enable Publisher Confirms on the channel options
var channelOptions = new CreateChannelOptions(
    publisherConfirmationsEnabled: true,
    publisherConfirmationTrackingEnabled: true
);

await using var channel = await connection.CreateChannelAsync(channelOptions);

var exchangeName = "demo.exchange";
var queueName1 = "demo.queue.1";
var queueName2 = "demo.queue.2";
var routingKey = "demo.key";

await channel.ExchangeDeclareAsync(
    exchange: exchangeName,
    type: ExchangeType.Direct   ,
    durable: true,
    autoDelete: false
);

await channel.QueueDeclareAsync(
    queue: queueName1,
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null
);

await channel.QueueDeclareAsync(
    queue: queueName2,
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null
);

await channel.QueueBindAsync(
    queue: queueName1,
    exchange: exchangeName,
    routingKey: routingKey
);

await channel.QueueBindAsync(
    queue: queueName2,
    exchange: exchangeName,
    routingKey: routingKey
);

while (true)
{
    Console.Write("> ");
    var message = Console.ReadLine() ?? string.Empty;
    if (string.IsNullOrWhiteSpace(message)) break;

    var body = Encoding.UTF8.GetBytes(message);

    // Instantiate properties directly in v7+
    var props = new BasicProperties
    {
        DeliveryMode = DeliveryModes.Persistent, // Persistent message
        MessageId = Guid.NewGuid().ToString()
    };

    try
    {
        // Publish asynchronously
        await channel.BasicPublishAsync(
            exchange: exchangeName,
            routingKey: routingKey,
            mandatory: true,
            basicProperties: props,
            body: body
        );

        Console.WriteLine($"✓ Published: {message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Message NOT confirmed by broker. Error: {ex.Message}");
    }
}

Console.WriteLine("\nExiting producer.");