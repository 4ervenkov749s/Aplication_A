using Aplication_A.BL.Interface;
using Aplication_A.BL.RabbitMq;
using Aplication_A.Models;
using MessagePack;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Threading.Tasks;

namespace Aplication_A.BL.Services
{
    public class RabbitMqService : IRabbitMqService, IDisposable
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;

        public RabbitMqService(IOptionsMonitor<RabbitMqConfig> rabbitMqconfig)
        {
            var factory = new ConnectionFactory()
            {
                HostName = rabbitMqconfig.CurrentValue.Host,
                Port = rabbitMqconfig.CurrentValue.Port
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare("person", ExchangeType.Direct, durable: true);

            _channel.QueueDeclare("person", durable: true, exclusive: false, autoDelete: false);
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }

        public async Task SendPersonAsync(Person p)
        {
            await Task.Factory.StartNew(() =>
            {
                //var serialize = JsonConvert.SerializeObject(p);
                //var body = Encoding.UTF8.GetBytes(serialize);

                var body = MessagePackSerializer.Serialize(p);

                _channel.BasicPublish("", "person", body: body);

                // da izpolzvam mesagepackserserializer
            });
        }
    }
}
