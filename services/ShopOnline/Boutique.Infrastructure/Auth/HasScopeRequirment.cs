using Microsoft.AspNetCore.Authorization;

namespace Boutique.Infrastructure.Auth
{
    public class HasScopeRequirment : IAuthorizationRequirement
    {
        public readonly string _scope;
        public readonly string _issuer;

        public HasScopeRequirment(string scope, string issuer)
        {
            _scope = scope;
            _issuer = issuer;
        }
    }
}