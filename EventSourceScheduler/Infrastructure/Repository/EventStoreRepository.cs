﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using EventSourceScheduler.Infrastructure.PortAdapters;
using RabbitMQ.Interface;
using SqlServices.Dapper;

namespace EventSourceScheduler.Infrastructure.Repository
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private const string TableName = "BoutiqueEventStore";
        private readonly SqlConnection _sqlConnection;
        private readonly DapperContribExtension<EventMessage> _eventStore;
        public EventStoreRepository(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
            _eventStore = new DapperContribExtension<EventMessage>(sqlConnection);
        }

        public void Save(EventMessage message)
        {
            _eventStore.AsInsert(message);
        }

    }
}
