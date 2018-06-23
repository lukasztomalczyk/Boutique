using System;
using System.Collections.Generic;
using System.Text;

namespace Boutique.Infrastructure.Auth
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public int ExpiryMinutes { get; set; }
        public string Issuer { get; set; }
    }
}
