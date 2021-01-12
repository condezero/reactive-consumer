using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reactive.Streaming.Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton(c =>
                    {
                        var conf = new ConsumerConfig
                        {
                            BootstrapServers ="localhost:9092",
                            GroupId = "booking_consumer",
                            EnableAutoCommit = false,
                            EnablePartitionEof = true,
                            AutoOffsetReset = AutoOffsetReset.Earliest
                        };
                        return new ConsumerBuilder<Null,string>(conf).Build();
                    });
                    services.AddSingleton<IBookingConsumer, BookingConsumer>();
                    services.AddSingleton<IBookingStream, BookingStream>();
                    services.AddHostedService<Worker>();
                });
    }
}
