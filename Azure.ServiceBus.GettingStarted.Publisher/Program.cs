using Bogus;
using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;

namespace Azure.ServiceBus.GettingStarted.Publisher
{
    class Program
    {
        async static Task Main(string[] args)
        {
            var connectionString = args[0];
            var queueName = "queue1";
            var topicName = "topic1";

            ISenderClient queueClient = new QueueClient(connectionString, queueName);
            ISenderClient topicClient = new TopicClient(connectionString, topicName);

            Console.WriteLine("Press any key to send a new random contact to queue and topic... Press Ctrl+C to exit");

            while (true)
            {
                Console.ReadKey(true);

                var messageText = GetRandomContact();
                var body = Encoding.UTF8.GetBytes(messageText);
                var message = new Message(body);

                Console.WriteLine($"Sending contact  {messageText}...");

                await queueClient.SendAsync(message);
                Console.WriteLine($"Contact has been sent to queue\r\n");

                await topicClient.SendAsync(message);
                Console.WriteLine($"Contact has been sent to topic\r\n");
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
