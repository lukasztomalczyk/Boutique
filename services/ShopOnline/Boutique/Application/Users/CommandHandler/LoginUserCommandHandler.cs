using Boutique.Domain.Users;
using Boutique.Infrastructure.Auth;
using Boutique.Infrastructure.CQRS.Commands;
using Boutique.Presentation.Commands.Auth;

namespace Boutique.Application.Users.CommandHandler
{
    public class LoginUserCommandHandler : IDomainCommandHandler<LoginCommand, JsonWebToken>
    {
        private readonly IUserService _userService;

        public LoginUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public JsonWebToken Handle(LoginCommand command)
        {
            return _userService.Login(command).Result;
        }
    }
}