using System;
using RabbitMQ.Client;

namespace RabbitMQ.Interface
{
    public interface IConnectionEventBus : IDisposable
    {
        IModel CreateSession();
        bool TryConnect();
        bool IsConnected();
    }
}