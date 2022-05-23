using Aplication_A.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aplication_A.BL.Kafka
{
    public class KafkaConsumer : IHostedService
        {
            private static IConsumer<int, Person> _consumer;

            public KafkaConsumer()
            {
                var config = new ConsumerConfig
                {
                    BootstrapServers = "localhost",
                    AutoCommitIntervalMs = 5000,
                    FetchWaitMaxMs = 50,
                    GroupId = Guid.NewGuid().ToString(),
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    EnableAutoCommit = true,
                    ClientId = "2",
                };

                _consumer = new ConsumerBuilder<int, Person>(config)
                    .SetValueDeserializer(new MsgPackDeserializer<Person>())
                    .Build();
            }

            public Task StartAsync(CancellationToken cancellationToken)
            {

                _consumer.Subscribe("test");
                Task.Factory.StartNew(() =>
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        try
                        {
                            var cr = _consumer.Consume(cancellationToken);
                            Console.WriteLine($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}' . ");
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }, cancellationToken);

                return Task.CompletedTask;
            }

            public Task StopAsync(CancellationToken cancellationToken)
            {
                return Task.CompletedTask;
            }
        }

    }

