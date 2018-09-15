using System.Collections.Generic;

namespace RabbitMQ.Settings
{
    public class RabbitMqSettings
    {
        public string Uri { get; set; }
        public string HostName { get; set; }
        public string QueueName { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public byte DeliveryMode { get; set; }
        public string ContentType { get; set; }
        public bool Persistent { get; set; }
        public bool AutoDelete { get; set; }
    }
}