using Boutique.Domain.Users;
using Boutique.Presentation.Commands.Auth;

namespace Boutique.Domain
{
    public interface IUserRepository
    {
        string Save(User user);
        User Load(string login);
        bool Contains(string login);
    }
}