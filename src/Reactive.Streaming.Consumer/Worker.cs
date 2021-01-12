using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reactive.Streaming.Consumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBookingStream _bookingStream;
        private readonly IBookingConsumer _bookingConsumer;

        public Worker(IBookingStream bookingStream, IBookingConsumer bookingConsumer,ILogger<Worker> logger)
        {
            _bookingStream = bookingStream;
            _bookingConsumer = bookingConsumer;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bookingStream.Subscribe("Subscriber1", (m) => Console.WriteLine($"Subscriber1 Message: {m.Message}"));
            _bookingStream.Subscribe("Subscriber12", (m) => Console.WriteLine($"Subscriber2 Message: {m.Message}"));
            _bookingConsumer.Listen(stoppingToken);
        }
    }
}
