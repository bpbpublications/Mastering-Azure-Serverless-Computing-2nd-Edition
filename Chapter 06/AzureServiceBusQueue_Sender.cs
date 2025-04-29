using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace servicebus_sender
{
    class Program
    {
        const string connectionString = "Connection string for Service Bus";
        const string queueName = "demoqueue";
        static IQueueClient queueClient;

        public static async Task Main(string[] args)
        {

            //  Create Queue Client object
            queueClient = new QueueClient(connectionString, queueName);

            for (var i = 1; i <= 10; i++)
            {
               //  Build Message for Queue
               string messageBody = $"Message # {i}";
               var message = new Message(Encoding.UTF8.GetBytes(messageBody));
               Console.WriteLine($"Sending message: {messageBody}");

               // Send the message to the queue
               await queueClient.SendAsync(message);
            }

            Console.ReadKey();
            // Close Connection
            await queueClient.CloseAsync();
        }
    }
}
