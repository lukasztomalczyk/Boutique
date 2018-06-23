using System;
using System.Data.SqlClient;
using Boutique.Domain;
using Boutique.Domain.Users;
using Boutique.Infrastructure.Attributes;
using Boutique.Infrastructure.Extensions;

namespace Boutique.Infrastructure.Repositories
{
    [Repository]
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnection _sqlConnection;

        public UserRepository(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }
        public string Save(string login, string password, string firstName, string lastName, string role)
        {
            var guid = Guid.NewGuid();
            var register = _sqlConnection.ExecuteQuery(
                $"INSERT INTO Users (Id, Login, Password, FirstName, LastName, Role)" +
                $"VALUES ({guid}, {login}, {password}, {firstName}, {lastName}, {role});");

            return register;
        }
    }
}