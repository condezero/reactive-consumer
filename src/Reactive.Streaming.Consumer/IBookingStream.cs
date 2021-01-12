using System;

namespace Reactive.Streaming.Consumer
{
    public interface IBookingStream: IDisposable
    {
        void Publish(BookingMessage bookingMessage);
        void Subscribe(string subscriberName, Action<BookingMessage> action);

        
    }
}
