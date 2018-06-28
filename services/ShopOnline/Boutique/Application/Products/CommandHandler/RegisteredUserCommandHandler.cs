using Boutique.Domain;
using Boutique.Infrastructure.Auth;
using Boutique.Infrastructure.CQRS.Commands;
using Boutique.Presentation.Commands.Auth;

namespace Boutique.Application.Products.CommandHandler
{
    public class RegisteredUserCommandHandler : IDomainCommandHandler<RegisterCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public RegisteredUserCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }
        public string Handle(RegisterCommand command)
        {
            var userId = _userRepository.Save(command.Login, command.Password, command.FirstName, command.LastName, command.Role);
            var tokenAccess = _jwtProvider.Create(userId, command.Role);

            return tokenAccess.AccessToken;
        }
    }
}