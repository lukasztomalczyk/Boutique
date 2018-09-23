using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Interface
{
    public interface IRabbitMqReadClient
    {
        void Read(Action<string> callbackAction, string queueName);
    }
}
