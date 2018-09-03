using System.Collections.Generic;

namespace RabbitMQ.Settings
{
    public class RabbitMqSettings
    {
        public string QueueName { get; set; }
        public string Name { get; set; }
        public List<ConnectionSettings> ConnectionSettings { get; set; }
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
}