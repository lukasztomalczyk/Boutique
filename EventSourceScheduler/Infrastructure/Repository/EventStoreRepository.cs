using System;
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

        public EventStoreRepository(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public void Save(EventMessage message)
        {
            var insertData = CreateModel(message);
            _sqlConnection.AsInsert(insertData, TableName);
        }

        private Dictionary<string,object> CreateModel(EventMessage message)
        {
            return new Dictionary<string, object>
            {
                {"Type",message.Type },
                {"AdditiopnalData", message.AdditionalData },
                {"CreatedAt",DateTime.Now }
            };
        }
    }
}
