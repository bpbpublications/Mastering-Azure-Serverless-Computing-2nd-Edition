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
        const string connectionString = "Provide Connection String";
        const string topicName = "topicdemo";
        static ITopicClient topicClient;

        public static async Task Main(string[] args)
        {
            topicClient = new TopicClient(connectionString, topicName);

            for (var i = 1; i <= 10; i++)
            {
               //  Build Message for Queue
               string messageBody = $"Message # {i}";
               var message = new Message(Encoding.UTF8.GetBytes(messageBody));
               Console.WriteLine($"Sending message: {messageBody}");

               // Send the message to the queue
               await topicClient.SendAsync(message);
            }

            Console.ReadKey();
            // Close Connection
            await topicClient.CloseAsync();
        }
    }
}
