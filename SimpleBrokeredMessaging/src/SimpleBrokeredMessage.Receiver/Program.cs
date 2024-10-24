using Azure.Messaging.ServiceBus;
var connectionString = args[0];
var queueName = "queue1";

await using (ServiceBusClient client = new ServiceBusClient(connectionString))
{
    // Create a receiver for the queue
    ServiceBusReceiver receiver = client.CreateReceiver(queueName);

    try
    {
        // Receive a message from the queue
        ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();

        // Process the message (for example, print it out)
        string body = receivedMessage.Body.ToString();
        Console.WriteLine($"Received message: {body}");

        // Complete the message, meaning it's been processed successfully
        await receiver.CompleteMessageAsync(receivedMessage);
        Console.WriteLine("Message completed successfully.");
    }
    catch (ServiceBusException ex)
    {
        Console.WriteLine($"Error occurred while receiving message: {ex.Message}");
    }
    finally
    {
        // Dispose of the receiver and client
        await receiver.DisposeAsync();
    }
}