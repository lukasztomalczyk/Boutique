

namespace Boutique.Infrastructure.Auth
{
    public interface IJwtProvider
    {
        JsonWebToken Create(string userId, string role);
    }
}
