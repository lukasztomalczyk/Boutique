using System.Threading.Tasks;
using Boutique.Infrastructure.Auth;
using Boutique.Presentation.Commands.Auth;

namespace Boutique.Domain.Interface
{
    public interface IUserService
    {
        Task<JsonWebToken> Login(LoginCommand command);
        Task<string> RegisterUser(RegisterCommand command);
    }
}
