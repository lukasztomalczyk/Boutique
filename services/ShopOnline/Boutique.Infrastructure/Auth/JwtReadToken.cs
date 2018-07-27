using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Boutique.Infrastructure.Auth
{
    public class JwtReadToken
    {
        public void ReadToken(string token)
        {
            var jwtToken = new JwtSecurityTokenHandler();
            var handlder = jwtToken.ReadJwtToken(token);
            var Claims = handlder.Claims;
        }
    }
}