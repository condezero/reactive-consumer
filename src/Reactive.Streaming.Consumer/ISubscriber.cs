using System.Threading.Tasks;

namespace Reactive.Streaming.Consumer
{
    public interface  ISubscriber
    {
            string SubscriberName {get;}
            Task DoSomethingAsync(BookingMessage message);
    }
}
