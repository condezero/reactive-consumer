using System.Threading;

namespace Reactive.Streaming.Consumer
{
    public interface IBookingConsumer
    {
        void Dispose();
        void Listen(CancellationToken cancellationToken);
    }
}