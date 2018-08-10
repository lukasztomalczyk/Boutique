using System;
using RabbitMQ.Client;

namespace Boutique.Messages.EventBusRabbitMQ
{
    public interface IRabbitMqConnection : IDisposable
    {
        bool IsConnected { get; }
        IModel CreateModel();
        bool TryConnect();
    }
}