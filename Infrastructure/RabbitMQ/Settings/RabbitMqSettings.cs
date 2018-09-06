using System.Collections.Generic;

namespace RabbitMQ.Settings
{
    public class RabbitMqSettings
    {
        public string QueueName { get; set; }
        public string Name { get; set; }
        public ConnectionSettings[] ConnectionSettings { get; set; }
        public QueueSettings[] QueueSettings { get; set; }
   }

    public class ConnectionSettings
    {
        public string HostAddress { get; set; }
        public string ServerName { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public int Port { get; set; }
    }

    public class QueueSettings
    {
        public byte DeliveryMode { get; set; }
        public string ContentType { get; set; }
        public bool Persistent { get; set; }
    }
}    