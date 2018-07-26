using System;
using System.Security.Cryptography;
using Boutique.Infrastructure.Extensions;

namespace Boutique.Infrastructure.Services.Password
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 20;
        
        public bool VerifyHashedPassword(string sendPassword, string userPassword)
        {
            return (sendPassword==userPassword) ? true : false;
        }

        public string HashPassword(string password)
        {
            return password;
        }
    }
}