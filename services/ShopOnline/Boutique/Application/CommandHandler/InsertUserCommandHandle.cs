using Boutique.Domain;
using Boutique.Infrastructure.CQRS.Commands;
using Boutique.Presentation.Commands.Auth;

namespace Boutique.Application.CommandHandler
{
    public class InsertUserCommandHandle : ICommandHandler<RegisterCommand, string>
    {
        private readonly IUserRepository _userRepository;

        public InsertUserCommandHandle(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public string Handle(RegisterCommand command)
        {
            return _userRepository.InsertUser(command.Id, command.Login, command.Password, command.FirstName, command.LastName, command.Role);
        }
    }
}