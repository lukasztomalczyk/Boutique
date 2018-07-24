

namespace Boutique.Infrastructure.Auth
{
    public interface IJwtProvider
    {
        JsonWebToken Create(string userLogin, string userId, string userRole);
    }
}
