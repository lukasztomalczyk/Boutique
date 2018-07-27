using System;
using System.Security.Cryptography;
using Boutique.Infrastructure.Extensions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Boutique.Infrastructure.Services.Password
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 128;
        private const int HashSize = 20;
        
        public bool VerifyHashedPassword(string sendPassword, string userPassword)
        {
            return (sendPassword==userPassword) ? true : false;
        }

        public string HashPassword(string password)
        {
            byte[] salt = new byte[SaltSize];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return  Convert.ToBase64String(KeyDerivation.Pbkdf2(
                            password: password,
                            salt: salt,
                            prf: KeyDerivationPrf.HMACSHA1,
                            iterationCount: 10000,
                            numBytesRequested: 256 / 8));
        }
    }
}