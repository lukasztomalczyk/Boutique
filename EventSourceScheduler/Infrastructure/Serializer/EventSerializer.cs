using EventSourceScheduler.Infrastructure.PortAdapters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourceScheduler.Infrastructure.Serializer
{
    public class EventSerializer
    {
        private static JSchema EventSchema = JSchema.Parse(@"{
                                                'Type': {'type': 'string'},
                                                'AdditionalData': {'type': 'string'}
                                            }");

        public static EventMessage TryDeserializeObject(string jsonData)
        {
            var jsonObject = JObject.Parse(jsonData);

            if (!jsonObject.IsValid(EventSchema)) return null;

            return JsonConvert.DeserializeObject<EventMessage>(jsonData);
             
        }
    }
}
