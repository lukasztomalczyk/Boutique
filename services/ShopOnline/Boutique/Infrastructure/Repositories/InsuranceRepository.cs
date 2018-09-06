using System;
using System.Data.SqlClient;
using Boutique.Domain.Insurances;
using Cqrs.Attributes;
using SqlServices.Dapper;

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

        public string Create(Insure insure)
        {
          //  var create = _sqlConnection.ExecuteQuery("");

            return Guid.NewGuid().ToString();
        }
    }
}