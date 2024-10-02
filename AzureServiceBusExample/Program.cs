using Azure.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;


class Program
{
    static string connectionString = "Your_Azure_Service_Bus_Connection_String";
    static string queueName = "samplequeue";

    static async Task Main(string[] args)
    {
        // Step 1: Create the Service Bus Client
        ServiceBusClient client = new ServiceBusClient(connectionString);

        // Sending the message
        ServiceBusSender sender = client.CreateSender(queueName);
        string messageBody = "Hello, Azure Service Bus!";
        ServiceBusMessage message = new ServiceBusMessage(messageBody);
        Console.WriteLine($"Sending message: {messageBody}");
        await sender.SendMessageAsync(message);
        Console.WriteLine("Message sent successfully!");

        // Step 2: Create the receiver for the queue
        ServiceBusReceiver receiver = client.CreateReceiver(queueName);

        // Step 3: Receive the message
        ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();
        Console.WriteLine($"Received message: {receivedMessage.Body}");

        // Step 4: Complete the message
        await receiver.CompleteMessageAsync(receivedMessage);
        Console.WriteLine("Message completed successfully!");

        // Dispose of sender, receiver, and client
        await sender.DisposeAsync();
        await receiver.DisposeAsync();
        await client.DisposeAsync();
    }
}



