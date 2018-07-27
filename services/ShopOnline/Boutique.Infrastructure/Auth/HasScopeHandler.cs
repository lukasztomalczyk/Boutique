using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Boutique.Infrastructure.Auth
{
    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirment requirement)
        {
            if(!context.User.HasClaim(c => c.Type == "sub"))
                return Task.CompletedTask;

            var scopes = context.User.FindAll(c => c.Type == "sub" && c.Issuer == requirement._issuer);
            if (scopes.Any(s=> s.Value == requirement._scope))
                context.Succeed(requirement);
            
            return Task.CompletedTask;
        }
    }
}