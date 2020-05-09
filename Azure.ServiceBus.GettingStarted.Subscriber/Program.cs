﻿using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace Azure.ServiceBus.GettingStarted.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = args[0];
            var queueName = "queue1";

            var queueClient = new QueueClient(connectionString, queueName);
            
            var messageHandlerOptions = new MessageHandlerOptions(OnException);
            queueClient.RegisterMessageHandler(OnMessage, messageHandlerOptions);
            
            Console.WriteLine("Listening, press any key");
            Console.ReadKey();
        }

        static Task OnMessage(Message m, CancellationToken ct)
        {
            var messageText = Encoding.UTF8.GetString(m.Body);
            Console.WriteLine("Got a message:");
            Console.WriteLine(messageText);
            Console.WriteLine($"Enqueued at {m.SystemProperties.EnqueuedTimeUtc:o}");
            return Task.CompletedTask;
        }

        static Task OnException(ExceptionReceivedEventArgs args)
        {
            Console.WriteLine("Got an exception:");
            Console.WriteLine(args.Exception.Message);
            Console.WriteLine(args.ExceptionReceivedContext.ToString());
            return Task.CompletedTask;
        }
    }
}
