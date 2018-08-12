

namespace Auth.Provider
{
    public interface IJwtProvider
    {
        JsonWebToken Create(string userLogin, string userId, string userRole);
    }
}
