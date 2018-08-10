using Boutique.Domain.Users;

namespace Boutique.Domain.Interface
{
    public interface IUserRepository
    {
        string Save(User user);
        User Load(string login);
        bool Contains(string login);
    }
}