using System;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Interface;
using RabbitMQ.Settings;

namespace RabbitMQ.Client
{
    public class RabbitMQWriteClient: IRabbitMQWriteClient
    {

        private readonly RabbitMqSettings _queueSettings;
        private IModel SessionChannel;

        public RabbitMQWriteClient(IModel connection, IOptions<RabbitMqSettings> queueSettings)
        {
            SessionChannel = connection;
            _queueSettings = queueSettings.Value;
        }

        public void Write(IEvent @event)
        {
            try
            {
                using (SessionChannel)
                {
                    var messageBody = Adapt(@event);

                    var props = SessionChannel.CreateBasicProperties();
                    props.ContentType = "text/plain";
                    props.DeliveryMode = (int)MqDeliveryModeEnum.Persistence;
                    props.ContentEncoding = Encoding.UTF8.EncodingName;
                    

                    SessionChannel.BasicPublish(exchange: _queueSettings.QueueName,
                        routingKey: "routing",
                        basicProperties: props,
                        body: messageBody);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        private byte[] Adapt(IEvent @event)
        {
            var message = JsonConvert.SerializeObject(@event);
            return Encoding.UTF8.GetBytes(message);
        }
    }
}