using System;
using RabbitMQ.Client;

namespace RabbitMQ.Interface
{
    public interface IConnectionEventBus : IDisposable
    {
        IModel CreateChannel();
    }
}