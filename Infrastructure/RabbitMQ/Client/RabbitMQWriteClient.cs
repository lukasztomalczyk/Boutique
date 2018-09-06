using System;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Interface;
using RabbitMQ.Settings;

namespace RabbitMQ.Client
{
    public class RabbitMqWriteClient: IRabbitMqWriteClient
    {

        private readonly RabbitMqSettings _queueSettings;
        private readonly IModel _sessionChannel;

        public RabbitMqWriteClient(IModel connection, IOptions<RabbitMqSettings> queueSettings)
        {
            _sessionChannel = connection;
            _queueSettings = queueSettings.Value;
        }

        public void Write(EventRoot @event)
        {
            try
            {
                using (_sessionChannel)
                {
                    var messageBody = Adapt(@event);
                    
                    _sessionChannel.ExchangeDeclare(exchange: _queueSettings.Name, type: "direct", durable: false);
                    _sessionChannel.QueueDeclare(queue: @event.EventScope, durable: false, exclusive: false, autoDelete: false);
                    
                    var props = _sessionChannel.CreateBasicProperties();
                        props.ContentType = _queueSettings.QueueSettings.First().ContentType;
                        props.DeliveryMode = _queueSettings.QueueSettings.First().DeliveryMode;
                        props.ContentEncoding = Encoding.UTF8.EncodingName;
                        props.Persistent = _queueSettings.QueueSettings.First().Persistent;
                    
                    _sessionChannel.BasicPublish(exchange: _queueSettings.Name, routingKey: @event.EventScope, basicProperties: props, body: messageBody);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        private byte[] Adapt(EventRoot @event)
        {
            var message = JsonConvert.SerializeObject(@event);
            return Encoding.UTF8.GetBytes(message);
        }
    }
}