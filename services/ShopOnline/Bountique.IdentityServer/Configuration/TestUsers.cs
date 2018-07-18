using System.Collections.Generic;
using System.Security.Claims;
using Boutique.Domain.Users;
using IdentityModel;
using IdentityServer4.Test;

namespace Boutique.Infrastructure.IdentityServer
{
    public class TestUsers
    {
        public static List<TestUser> Users = new List<TestUser>
        {
            new TestUser()
            {
                SubjectId = "111",
                Username = "Lukasz",
                Password = "mojehaslo",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Lukasz")
                }
            }
        };
    }
}