using System;
using System.Data.SqlClient;
using Boutique.Domain.Insurances;
using Cqrs.Attributes;

namespace Boutique.Infrastructure.Repositories
{
    [Repository]
    public class InsuranceRepository : IInsuranceRepository
    {
        private readonly SqlConnection _sqlConnection;

        public InsuranceRepository(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public string Save(Insurance insure)
        {
          //  var create = _sqlConnection.ExecuteQuery("");

            return Guid.NewGuid().ToString();
        }
    }
}