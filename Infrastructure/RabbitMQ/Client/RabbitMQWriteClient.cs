using System;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Interface;
using RabbitMQ.Settings;

namespace RabbitMQ.Client
{
    public class RabbitMQWriteClient: IRabbitMqWriteClient
    {

        private readonly RabbitMqSettings _queueSettings;
        private IModel SessionChannel;

        public RabbitMQWriteClient(IModel connection, IOptions<RabbitMqSettings> queueSettings)
        {
            SessionChannel = connection;
            _queueSettings = queueSettings.Value;
        }

        public void Write(EventRoot @event)
        {
            try
            {
                using (SessionChannel)
                {
                    var messageBody = Adapt(@event);
                    
                    SessionChannel.ExchangeDeclare(exchange: _queueSettings.QueueName, type: "direct", durable: true);
                    SessionChannel.QueueDeclare(queue: @event.EventScope, durable: true, exclusive: false,
                        autoDelete: false);
                    
//                    var props = SessionChannel.CreateBasicProperties();
//                    props.ContentType = "text/plain";
//                    props.DeliveryMode = (int)MqDeliveryModeEnum.Persistence;
//                    props.ContentEncoding = Encoding.UTF8.EncodingName;
                    

                    SessionChannel.BasicPublish(exchange: _queueSettings.QueueName,
                        routingKey: @event.EventScope,
                        basicProperties: null,
                        body: messageBody);
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