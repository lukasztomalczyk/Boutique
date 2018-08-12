using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Provider
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public int ExpiryMinutes { get; set; }
        public string Issuer { get; set; }
    }
}
