using System.Threading.Tasks;
using Boutique.Presentation.Commands.Auth;

namespace Boutique.Domain.Users
{
    public interface IUserService
    {
        Task<string> Login(LoginCommand command);
        Task<string> RegisterUser(RegisterCommand command);
    }
}
