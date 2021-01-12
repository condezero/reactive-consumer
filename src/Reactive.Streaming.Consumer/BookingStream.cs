using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Reactive.Streaming.Consumer
{
    public class BookingStream : IBookingStream, IDisposable
    {
        private Subject<BookingMessage> _bookingMessageSubject;
        private IDictionary<string, IDisposable> _subscribers;


        public BookingStream()
        {
            _bookingMessageSubject = new Subject<BookingMessage>();
            _subscribers = new Dictionary<string, IDisposable>();
        }

        public void Dispose()
        {
            if(_bookingMessageSubject is not null)
            {
                _bookingMessageSubject.Dispose();
            }
            foreach(var subscriber in _subscribers)
            {
                subscriber.Value.Dispose();
            }
        }

        public void Publish(BookingMessage bookingMessage)
        {
            _bookingMessageSubject.OnNext(bookingMessage);
        }

        public void Subscribe(string subscriberName, Action<BookingMessage> action)
        {
            if (!_subscribers.ContainsKey(subscriberName))
            {
                _subscribers.Add(subscriberName, _bookingMessageSubject.Where(m=> m.Message.Length > 0).Subscribe(action));
            }
        }
    }
}
