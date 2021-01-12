using System;
using System.Threading.Tasks;

namespace Reactive.Streaming.Consumer.Subscribers
{
    public class Subscriber1 : ISubscriber
    {
        public string SubscriberName => "Subscriber1";

        public Task DoSomethingAsync(BookingMessage message)
        {
            Console.WriteLine($"Subscriber1 Message: {message.Message}");
            return Task.CompletedTask;
        }
    }
}
