// See https://aka.ms/new-console-template for more information
using Azure.Messaging.ServiceBus;

//$topicName = "topic1"

//az servicebus topic create -g $resourceGroup ` --namespace-name $namespaceName -n $topicName

//az servicebus topic subscription create -g $resourceGroup --namespace-name $namespaceName --topic-name $topicName -n "subscription1" 

//az servicebus topic subscription create -g $resourceGroup --namespace-name $namespaceName --topic-name $topicName -n "subscription2"

var connectionString = args[0];
var topicName = "topic1";
var msg = args[1];
// Create a ServiceBusClient using the connection string
await using (ServiceBusClient client = new ServiceBusClient(connectionString))
{
    // Create a sender for the queue
    ServiceBusSender sender = client.CreateSender(topicName);

    try
    {
        // Create a message to send
        ServiceBusMessage message = new ServiceBusMessage(msg);

        // Send the message to the queue
        await sender.SendMessageAsync(message);

        Console.WriteLine("Message sent successfully.");
    }
    finally
    {
        // Dispose of the sender and client to free resources
        await sender.DisposeAsync();
    }
}

