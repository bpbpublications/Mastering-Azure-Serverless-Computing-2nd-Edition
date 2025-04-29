using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace servicebus_receiver
{
    class Program
    {
        const string connectionString = "Connection string for Service Bus";
        const string queueName = "demoqueue";
        static IQueueClient queueClient;


        static void Main(string[] args)
        {
            queueClient = new QueueClient(connectionString, queueName);

            //Message Handler Options. Register method that will handle exceptions
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            //Register the method that will read from Queue
            queueClient.RegisterMessageHandler(ReceiveMessagesAsync, messageHandlerOptions);
            Console.ReadKey();
            queueClient.CloseAsync();
        }


        static async Task ReceiveMessagesAsync(Message message, CancellationToken token)
        {
            Console.WriteLine($"Received message: {Encoding.UTF8.GetString(message.Body)}");
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine(exceptionReceivedEventArgs.Exception);
            return Task.CompletedTask;
        }
    }
}
