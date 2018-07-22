using Boutique.Domain.Users;
using Boutique.Infrastructure.CQRS.Commands;
using Boutique.Presentation.Commands.Auth;

namespace Boutique.Application.Users.CommandHandler
{
    public class LoginUserCommandHandler : IDomainCommandHandler<LoginCommand, string>
    {
        private readonly IUserService _userService;

        public LoginUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public string Handle(LoginCommand command)
        {
            return _userService.Login(command).Result;
        }
    }
}