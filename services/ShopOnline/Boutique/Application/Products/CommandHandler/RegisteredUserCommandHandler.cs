using Boutique.Domain;
using Boutique.Infrastructure.CQRS.Commands;
using Boutique.Presentation.Commands.Auth;

namespace Boutique.Application.Products.CommandHandler
{
    public class RegisteredUserCommandHandler : IDomainCommandHandler<RegisterCommand, string>
    {
        private readonly IUserRepository _userRepository;

        public RegisteredUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public string Handle(RegisterCommand command)
        {
            return _userRepository.Save(command.Login, command.Password, command.FirstName, command.LastName, command.Role);
        }
    }
}