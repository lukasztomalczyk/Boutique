using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boutique.Infrastructure.Auth
{
    public class AuthAttribute : AuthorizeAttribute
    {
        public AuthAttribute(string policy = "")
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
            Policy = policy;
        }
    }
}
