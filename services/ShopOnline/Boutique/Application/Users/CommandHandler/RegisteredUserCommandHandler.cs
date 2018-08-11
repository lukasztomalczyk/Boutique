using System;
using Boutique.Domain;
using Boutique.Domain.Users;
using Boutique.Infrastructure.Builders;
using Boutique.Infrastructure.CQRS.Commands;
using Boutique.Infrastructure.Repositories;
using Boutique.Presentation.Commands.Auth;

namespace Boutique.Application.Users.CommandHandler
{
    public class RegisteredUserCommandHandler : IDomainCommandHandler<RegisterCommand, string>
    {
        private readonly IUserRepository _userService;

        public RegisteredUserCommandHandler(IUserRepository userService)
        {
            _userService = userService;
        }
        public string Handle(RegisterCommand command)
        {
            var userBuild = new UserBuilder().Create()
                                            .WithId(Guid.NewGuid().ToString())
                                            .WithName(command.FirstName)
                                            .WithLastName(command.LastName)
                                            .WithPassword(command.Password)
                                            .WithRole(command.Role)
                                            .Build();
            return _userService.Save(userBuild);
        }
    }
}