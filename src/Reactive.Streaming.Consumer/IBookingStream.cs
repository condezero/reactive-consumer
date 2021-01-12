using System;

namespace Reactive.Streaming.Consumer
{
    public interface IBookingStream
    {
        void Publish(BookingMessage bookingMessage);
        void Subscribe(string subscriberName, Action<BookingMessage> action);
    }
}
