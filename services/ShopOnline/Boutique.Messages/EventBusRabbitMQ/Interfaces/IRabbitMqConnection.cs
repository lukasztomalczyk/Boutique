using System;
using RabbitMQ.Client;

namespace Boutique.Messages.EventBusRabbitMQ
{
    public interface IRabbitMqConnection
    {
        IModel InitializeSession();
        bool TryConnect();
    }
}