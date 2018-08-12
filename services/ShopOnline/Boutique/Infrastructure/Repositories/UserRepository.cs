using System.Data.SqlClient;
using System.Linq;
using Boutique.Domain.Interface;
using Boutique.Domain.Users;
using Boutique.Infrastructure.Builders;
using Cqrs.Attributes;
using Dapper;
using SqlServices.Dapper;

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

        public string Save(User user)
        {
            var register = _sqlConnection.ExecuteQuery(
                $"INSERT INTO Users (Id, Login, Password, FirstName, LastName, Role)" +
                $"VALUES ('{user.Id}', '{user.Login}', '{user.Password}', '{user.FirstName}', '{user.LastName}', '{user.Role}');");

            return user.Id;
        }

        public User Load(string login)
        {
            var rawUser = _sqlConnection.Query($"SELECT * FROM Users where Login = '{login}'", new { Login = login }).FirstOrDefault();

            return new UserBuilder()
                .WithId((string)rawUser.Id)
                .WithName((string)rawUser.Name)
                .WithLastName((string)rawUser.LastName)
                .WithLogin((string)rawUser.Login)
                .WithPassword((string)rawUser.Password)
                .WithRole((string)rawUser.Role)
                .Create();
        }

        public bool Contains(string login)
        {
            var result = _sqlConnection.QueryFirstOrDefault<int>($"SELECT COUNT(Login) FROM Users WHERE Login = '{login}'");
            return (result == 1) ? true : false;
        }
    }
}