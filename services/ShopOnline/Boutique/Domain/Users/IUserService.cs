using Boutique.Presentation.Commands.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Infrastructure.Auth.User
{
    public interface IUserService
    {
        Task<JsonWebToken> Login(LoginCommand command);
        Task<string> RegisterUser(RegisterCommand command);
    }
}
