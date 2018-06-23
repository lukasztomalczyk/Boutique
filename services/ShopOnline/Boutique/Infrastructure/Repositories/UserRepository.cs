using System;
using System.Data.SqlClient;
using System.Linq;
using Boutique.Domain;
using Boutique.Domain.Users;
using Boutique.Infrastructure.Attributes;
using Boutique.Infrastructure.Builders;
using Boutique.Infrastructure.Extensions;
using Dapper;

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

        public User LoadById(string Id)
        {
            var rawUser = _sqlConnection.Query("SELECT * FROM Users where Id = '@Id'", new { Id = Id }).FirstOrDefault();

            return new UserBuilder()
                .WithId((string)rawUser.Id)
                .WithName((string)rawUser.Name)
                .WithLastName((string)rawUser.LastName)
                .WithLogin((string)rawUser.Login)
                .WithPassword((string)rawUser.Password)
                .WithRole((string)rawUser.Role)
                .Create();

        }
    }
}