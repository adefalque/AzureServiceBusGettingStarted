using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace Azure.ServiceBus.GettingStarted.Subscriber
{
    class Program
    {
        const string queueName = "queue1";
        const string topicName = "topic1";
        const string firstSubName = "subscription1";
        const string secondSubName = "subscription2";
        private const ConsoleColor queueConsoleColor = ConsoleColor.Green;
        private const ConsoleColor sub1ConsoleColor = ConsoleColor.Magenta;
        private const ConsoleColor sub2ConsoleColor = ConsoleColor.Blue;

        private static object lockMessageHandle = new object();

        static void Main(string[] args)
        {
            var connectionString = args[0];

            var queueClient = new QueueClient(connectionString, queueName);
            queueClient.RegisterMessageHandler(
                (message, token) => HandleMessage(message, queueName, queueConsoleColor), 
                new MessageHandlerOptions(OnException));

            var firstSubscriptionClient = new SubscriptionClient(connectionString, topicName, firstSubName);
            firstSubscriptionClient.RegisterMessageHandler(
                (message, token) => HandleMessage(message, firstSubName, sub1ConsoleColor), 
                new MessageHandlerOptions(OnException));

            var secondSubscriptionClient = new SubscriptionClient(connectionString, topicName, secondSubName);
            secondSubscriptionClient.RegisterMessageHandler(
                (message, token) => HandleMessage(message, secondSubName, sub2ConsoleColor), 
                new MessageHandlerOptions(OnException));

            Console.WriteLine("Listening, press any key");
            Console.ReadKey();
        }

        static Task HandleMessage(Message m, string handlerName, ConsoleColor color)
        {
            lock (lockMessageHandle)
            {
                var messageText = Encoding.UTF8.GetString(m.Body);
                WriteLineColor($"[${handlerName}] Got a message:", color);
                WriteLineColor(messageText, color);
                WriteLineColor($"[${handlerName}] Enqueued at {m.SystemProperties.EnqueuedTimeUtc:o}", color);
                Console.WriteLine();
            }

            return Task.CompletedTask;
        }

        static Task OnException(ExceptionReceivedEventArgs args)
        {
            Console.WriteLine("Got an exception:");
            Console.WriteLine(args.Exception.Message);
            Console.WriteLine(args.ExceptionReceivedContext.ToString());
            return Task.CompletedTask;
        }

        static void WriteLineColor(string s, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(s);
            Console.ResetColor();
        }
    }
}
