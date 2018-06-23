using Boutique.Domain.Users;
using Boutique.Presentation.Commands.Auth;

namespace Boutique.Domain
{
    public interface IUserRepository
    {
        string InsertUser(string id, string login, string password, string firstName, string lastName, string role);
    }
}