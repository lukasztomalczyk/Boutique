using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourceScheduler.Infrastructure.Settings
{
    public class EventSourceSettings
    {
        public string ConnectionString { get; set; }
        public string QueueName { get; set; }
    }
}
