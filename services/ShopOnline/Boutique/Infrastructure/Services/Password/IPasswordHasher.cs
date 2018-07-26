namespace Boutique.Infrastructure.Services.Password
{
    public interface IPasswordHasher
    {
        bool VerifyHashedPassword(string sendPassword, string userPassword);
        string HashPassword(string password);
    }
}