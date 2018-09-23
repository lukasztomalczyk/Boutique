using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourceScheduler.Infrastructure.PortAdapters
{
    [Table("BoutiqueEventStore")]
    public class EventMessage
    {
        public string Type { get; set; }
        public string AdditionalData { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
