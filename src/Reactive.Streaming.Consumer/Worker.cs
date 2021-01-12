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

        public override void Dispose()
        {
            _bookingConsumer?.Dispose();
            _bookingStream?.Dispose();
            base.Dispose();
        }


        protected override  Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bookingConsumer.Listen(stoppingToken);

            return Task.CompletedTask;
        }
        
    }
}
