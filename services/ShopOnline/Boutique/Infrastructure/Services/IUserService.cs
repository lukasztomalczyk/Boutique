using System.Threading.Tasks;
using Boutique.Infrastructure.Auth;
using Boutique.Presentation.Commands.Auth;

namespace Boutique.Infrastructure.Services
{
    public interface IUserService
    {
        Task<JsonWebToken> Login(LoginCommand command);
    }
}
