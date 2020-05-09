using Bogus;
using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Azure.ServiceBus.GettingStarted.Publisher
{
    class Program
    {
        async static Task Main(string[] args)
        {
            var connectionString = args[0];
            var queueName = "queue1";

            var queueClient = new QueueClient(connectionString, queueName);

            Console.WriteLine("Press any key to send a new random contact to queue... Press Ctrl+C to exit");

            while (true)
            {
                Console.ReadKey(true);

                var messageText = GetRandomContact();
                var body = Encoding.UTF8.GetBytes(messageText);
                var message = new Message(body);

                Console.WriteLine("Sending contact...");
                await queueClient.SendAsync(message);
                
                Console.WriteLine($"Contact {messageText} has been sent to queue\r\n");
                Console.WriteLine($"Enqueued at {message.SystemProperties.EnqueuedTimeUtc:o}\r\n");
            }
        }

        private static string GetRandomContact()
        {
            var contact = new Faker<Contact>()
                .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName())
                .RuleFor(u => u.LastName, (f, u) => f.Name.LastName())
                .RuleFor(u => u.Avatar, f => f.Internet.Avatar())
                .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(u => u.PhoneOffice, (f, u) => f.Phone.PhoneNumber())
                .RuleFor(u => u.PhoneMobile, (f, u) => f.Phone.PhoneNumber())
                
                .Generate();

            return JsonConvert.SerializeObject(contact);
        }
    }
}
