// See https://aka.ms/new-console-template for more information
using Azure.Messaging.ServiceBus;

var connectionString = args[0];
var topicName = "topic1";
var subscriptionName = args[1];
await using (ServiceBusClient client = new ServiceBusClient(connectionString))
{
    // Create a receiver for the subscription
    ServiceBusReceiver receiver = client.CreateReceiver(topicName, subscriptionName);

    try
    {
        // Receive a message from the subscription
        ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();

        // Process the message
        string body = receivedMessage.Body.ToString();
        Console.WriteLine($"Received message: {body}");

        // Complete the message (mark it as successfully processed)
        await receiver.CompleteMessageAsync(receivedMessage);

        Console.WriteLine("Message completed successfully.");
    }
    catch (ServiceBusException ex)
    {
        Console.WriteLine($"Error occurred while receiving the message: {ex.Message}");
    }
    finally
    {
        await receiver.DisposeAsync();
    }
}
