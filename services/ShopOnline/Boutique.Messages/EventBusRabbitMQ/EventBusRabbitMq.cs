using System;
using System.Text;
using Boutique.Messages.EventBusRabbitMQ.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Impl;

namespace Boutique.Messages.EventBusRabbitMQ
{
    public class EventBusRabbitMq : IEventBus
    {
        //Broker do przeniesienia Settings
        private const string BROKER_NAME = "boutique_event_bus";
        private readonly IRabbitMqConnection _connectionRabbit;

        public EventBusRabbitMq(IRabbitMqConnection connectionRabbit)
        {
            _connectionRabbit = connectionRabbit;
        }
        
        public void Publish(IEvent @event)
        {
            if (!_connectionRabbit.TryConnect()) return;

            using (var mqSession = _connectionRabbit.InitializeSession())
            {
                //Butique do settingów
                var eventName = "butique." + @event.GetType().Name;
                
                mqSession.ExchangeDeclare(exchange: BROKER_NAME,
                                        type: "topic");

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);
                
                mqSession.BasicPublish(exchange: BROKER_NAME,
                                    routingKey: eventName,
                                    basicProperties: null,
                                    body: body);
            }
        }
    }
}