using System;
using System.Data.SqlClient;
using Boutique.Domain.Insure.Policy;
using Cqrs.Attributes;
using Dapper.Contrib.Extensions;

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

        public string Create(Insurance insurance)
        {
            var create = _sqlConnection.Insert(insurance);

            return insurance.Id;
        }
    }
}