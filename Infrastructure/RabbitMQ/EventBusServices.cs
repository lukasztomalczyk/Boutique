﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Interface;

namespace RabbitMQ
{
    public class EventBusServices : IEventBusServices
    {
        private readonly IConnectionEventBus _connectionEventBus;
        private IModel _model;
        private const string  EventBusName = "boutique_event_bus";

        public EventBusServices(IConnectionEventBus connectionEventBus)
        {
            _connectionEventBus = connectionEventBus;

            _model = _connectionEventBus.CreateChannel();
        }

        public void Publish(IEvent @event, string queueName)
        {
            using (_connectionEventBus)
            {
                using (_model)
                {
                    ExchangeDeclare(_model);
                    _model.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false);
                    PublishEvent(@event, queueName, _model);
                }
            }
        }

        public string Subscribe(string queueName)
        {
            var routing = queueName + ".*";
            using (_connectionEventBus)
            {
                using (_model)
                {
                     ExchangeDeclare(_model);
                    _model.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false);
                    _model.QueueBind(queue: queueName, exchange: EventBusName, routingKey: routing);

                    var consumer = new EventingBasicConsumer(_model);

                    dynamic message = "";
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var json = Encoding.UTF8.GetString(body);
                        message = JsonConvert.DeserializeObject(json);
                        var routingKey = ea.RoutingKey;

                    };

                    _model.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                    return message.ToString();
                }
            }
        }

        private void PublishEvent(IEvent @event, string queueName, IModel channel)
        {
            var routing = Routing(@event, queueName);

            channel.BasicPublish(exchange: EventBusName,
                routingKey: routing,
                basicProperties: null,
                body: ConvertEventToSend(@event));
        }

        private string Routing(IEvent @event, string queueName)
        {
            return queueName + "." + @event.GetType().Name;
        }

        private void ExchangeDeclare(IModel channel)
        {
    
            channel.ExchangeDeclare(exchange: EventBusName,
                type: "topic",
                durable: false,
                autoDelete: false,
                arguments: null);
        }

        private static byte[] ConvertEventToSend(IEvent @event)
        {
            var message = JsonConvert.SerializeObject(@event);
           return Encoding.UTF8.GetBytes(message);
        }
    }
}