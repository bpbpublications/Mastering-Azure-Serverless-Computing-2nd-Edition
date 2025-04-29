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
        const string connectionString = "Provide connection string";
        const string topicName = "topicdemo";
        const string subscriptionName = "subscription1";
        static ISubscriptionClient subscriptionClient;
        static void Main(string[] args)
        {
            subscriptionClient = new SubscriptionClient(connectionString, topicName, subscriptionName);

            //Message Handler Options. Register method that will handle exceptions
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            //Register the method that will read from Queue
            subscriptionClient.RegisterMessageHandler(ReceiveMessagesAsync, messageHandlerOptions);
            Console.ReadKey();
            subscriptionClient.CloseAsync();
        }
        static async Task ReceiveMessagesAsync(Message message, CancellationToken token)
        {
            Console.WriteLine($"Received message: {Encoding.UTF8.GetString(message.Body)}");
            await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine(exceptionReceivedEventArgs.Exception);
            return Task.CompletedTask;
        }
    }
}
