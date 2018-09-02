using Cqrs.Interface;
using EventSourceScheduler.Infrastructure.PortAdapters;
using EventSourceScheduler.Infrastructure.Repository;
using EventSourceScheduler.Infrastructure.Serializer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourceScheduler.Application.Handlers
{
    public class EventSoruceHandler : IListenerHandler
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public EventSoruceHandler(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public void Handle(string message)
        {
            var eventMessage = EventSerializer.TryDeserializeObject(message);

            if (eventMessage == null) return;

            _eventStoreRepository.Save(eventMessage);

        }
    }
}
