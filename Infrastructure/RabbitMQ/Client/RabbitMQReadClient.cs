using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using RabbitMQ.Interface;
using RabbitMQ.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Client
{
    public class RabbitMqReadClient : IRabbitMqReadClient
    {

        private readonly RabbitMqSettings _queueSettings;
        private readonly IModel _sessionChannel;

        public RabbitMqReadClient(IModel connection, IOptions<RabbitMqSettings> queueSettings)
        {
            _sessionChannel = connection;
            _queueSettings = queueSettings.Value;
        }

        public void Read(Action<string> callBAck, string queueName)
        {
            _sessionChannel.ExchangeDeclare("direct", ExchangeType.Direct);

            _sessionChannel.QueueBind(queueName, ExchangeType.Direct, routingKey: queueName, null);
            var subscription = new Subscription(_sessionChannel, queueName,false);
            Task.Run(() =>
            {
                while (true)
                {
                    var consumer = subscription.Next();
                    subscription.Ack(consumer);
                    if(consumer != null)
                    {
                        var message = Encoding.Default.GetString(consumer?.Body);
                        callBAck(message);
                    }
                 
                }
            });
        }

    }
}
