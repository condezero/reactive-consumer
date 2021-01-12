using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace Reactive.Streaming.Consumer
{
    public class BookingConsumer : IBookingConsumer, IDisposable
    {
        private readonly IConsumer<Null, string> _consumer;
        private readonly IBookingStream _bookingStream;
        private readonly ILogger<BookingConsumer> _logger;

        public BookingConsumer(IConsumer<Null, string> consumer, IBookingStream bookingStream, ILogger<BookingConsumer> logger)
        {
            _consumer = consumer;
            _bookingStream = bookingStream;
            _logger = logger;
            _consumer.Subscribe("test_booking");
        }

        public void Dispose()
        {
            _consumer?.Dispose();
        }

        public void Listen(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.Consume(cancellationToken);
                    if (consumeResult.IsPartitionEOF)
                    {
                        _logger.LogInformation(
                            $"Reached end of topic {consumeResult.Topic}, partition {consumeResult.Partition}, offset {consumeResult.Offset}.");
                        continue;
                    }
                    _bookingStream.Publish(new BookingMessage { Message = consumeResult.Message.Value });
                }
                catch (ConsumeException ex)
                {
                    _logger.LogError(ex, "Error on consumme message");
                }
                catch (KafkaException ex)
                {
                    _logger.LogError(ex, "Error on kafka");
                }
            }

        }
    }
}
