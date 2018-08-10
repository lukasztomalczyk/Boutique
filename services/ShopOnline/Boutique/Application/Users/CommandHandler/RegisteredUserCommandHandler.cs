using Boutique.Domain;
using Boutique.Domain.Interface;
using Boutique.Domain.Users;
using Boutique.Infrastructure.CQRS.Commands;
using Boutique.Presentation.Commands.Auth;

namespace Boutique.Application.Users.CommandHandler
{
    public class RegisteredUserCommandHandler : IDomainCommandHandler<RegisterCommand, string>
    {
        private readonly IUserService _userService;

        public RegisteredUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public string Handle(RegisterCommand command)
        {
            return _userService.RegisterUser(command).Result;
        }
    }
}