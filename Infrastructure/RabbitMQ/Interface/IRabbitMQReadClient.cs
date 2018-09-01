using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Interface
{
    public interface IRabbitMQReadClient
    {
        string Read(string queueName);
    }
}
