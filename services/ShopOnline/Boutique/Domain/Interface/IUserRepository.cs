using Boutique.Domain.Users;

namespace Boutique.Domain.Products
{
    public interface IUserRepository
    {
        string Save(User user);
        User Load(string login);
        bool Contains(string login);
    }
}