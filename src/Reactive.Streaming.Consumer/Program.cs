using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using Reactive.Streaming.Consumer.Subscribers;

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
                    services.AddSubscribers();
                    
                    
                    services.AddHostedService<Worker>();
                });
    }
    public static class ServiceCollectionExtensions {

        public static void AddSubscribers(this IServiceCollection services){
            
            services.AddSingleton<ISubscriber, Subscriber1>();
            services.AddSingleton<ISubscriber, Subscriber2>();

            services.AddSingleton<IBookingStream>(c=>{
                        var subscribers = c.GetRequiredService<IEnumerable<ISubscriber>>();
                        var bStream = new BookingStream();
                        foreach(var sub in subscribers){
                            bStream.Subscribe(sub.SubscriberName, async (m)=>await sub.DoSomethingAsync(m));
                        }
                        return bStream;
                    });
            
        }

    }
}
