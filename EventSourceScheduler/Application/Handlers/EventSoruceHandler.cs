using Cqrs.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourceScheduler.Application.Handlers
{
    public class EventSoruceHandler : IListenerHandler
    {
        public EventSoruceHandler()
        {

        }
        public void Handle(string message)
        {
            throw new NotImplementedException();
        }
    }
}
