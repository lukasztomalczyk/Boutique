using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Interface;
using RabbitMQ.Settings;

namespace RabbitMQ
{
    public class RabbitMQWriteClient: IRabbitMQWriteClient
    {
        private  IConnection _connection;
        private readonly EventQueueSettings _queueSettings;
        private IModel SessionChannel;

        public RabbitMQWriteClient(IConnection connection, IOptions<EventQueueSettings> queueSettings)
        {
            _connection = connection;
            _queueSettings = queueSettings.Value;
        }

        public void Write(IEvent @event)
        {
            try
            {
                using (_connection)
                using (SessionChannel = _connection.CreateModel())
                {
                    var messageBody = Adapt(@event);

                    var props = SessionChannel.CreateBasicProperties();
                    props.ContentType = "text/plain";
                    props.DeliveryMode = 2;
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