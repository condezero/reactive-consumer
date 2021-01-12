using System;
using System.Threading.Tasks;

namespace Reactive.Streaming.Consumer.Subscribers
{
    public class Subscriber2 : ISubscriber
    {
        public string SubscriberName => "Subscriber2";

        public Task DoSomethingAsync(BookingMessage message)
        {
            Console.WriteLine($"Subscriber2 Message: {message.Message}");
            return Task.CompletedTask;
        }
    }
}
