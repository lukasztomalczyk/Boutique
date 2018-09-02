using EventSourceScheduler.Infrastructure.PortAdapters;
using RabbitMQ.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourceScheduler.Infrastructure.Repository
{
    public interface IEventStoreRepository
    {
        void Save(EventMessage message);
    }
}
