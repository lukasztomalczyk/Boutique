using System;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Boutique.Domain;
using Boutique.Domain.Users;
using Boutique.Domain.Users.Event;
using Boutique.Infrastructure.Attributes;
using Boutique.Infrastructure.Auth;
using Boutique.Infrastructure.DDD;
using Boutique.Infrastructure.Repositories;
using Boutique.Infrastructure.Settings;
using Boutique.Presentation.Commands.Auth;
using Microsoft.Extensions.Options;
using Boutique.Infrastructure.Services.Password;


namespace Boutique.Infrastructure.Services
{
    [Services]
    public class UserService : IUserService
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IDomainEventDispatcher _eventDispatcher;

        public UserService(IJwtProvider jwtProvider, IUserRepository userRepository, IDomainEventDispatcher eventDispatcher)
        {
            _jwtProvider = jwtProvider;
            _passwordHasher = new PasswordHasher();
            _userRepository = userRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<JsonWebToken> Login(LoginCommand command)
        {
            if (!IsUserExists(command.UserName))
                throw new Exception("User dont exists");
            
            var user = _userRepository.Load(command.UserName);
            var hashPassword = _passwordHasher.HashPassword(command.Password);
            
            if(!_passwordHasher.VerifyHashedPassword(hashPassword, user.Password))
                throw new Exception("Password not match");
            
            return await Task.FromResult(_jwtProvider.Create(user.Login, user.Id, user.Role));
        }

        public bool IsUserExists(string userName)
        {
            return  _userRepository.Contains(userName);
        }       
    }

}
