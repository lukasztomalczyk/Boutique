using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Interface
{
    public interface IRabbitMqReadClient
    {
        string Read(string queueName);
    }
}
