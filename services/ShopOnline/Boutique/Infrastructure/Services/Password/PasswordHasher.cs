using System;
using System.Security.Cryptography;
using System.Text;
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
            using (var md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(password));
                
                return Convert.ToBase64String(data);
            }
        }
    }
}