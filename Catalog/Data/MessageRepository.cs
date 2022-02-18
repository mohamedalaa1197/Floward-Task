using Catalog.Interfaces;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Data
{
    public class MessageRepository : IMessageRepository
    {
        public async Task SendMessage(string productName, decimal productPrice)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://guest:guest@localhost:5672");

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.ExchangeDeclare("productEx", ExchangeType.Fanout, true);

            var message = $"{productName};{productPrice}";
            var bytes = System.Text.Encoding.UTF8.GetBytes(message);
            channel.BasicPublish("productEx", "", null, bytes);
            channel.Close();
            connection.Close();

        }
    }
}
