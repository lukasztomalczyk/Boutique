using Boutique.Domain;
using Boutique.Domain.Users;
using Boutique.Infrastructure.Attributes;
using Boutique.Infrastructure.Auth;
using Boutique.Infrastructure.Auth.User;
using Boutique.Presentation.Commands.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Infrastructure.Repositories
{
    [Services]
    public class UserService : IUserService
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserRepository _userRepository;

        /// 1. Repository dla user
        /// 2. User isExits
        /// 3. Password Comapre
        /// 

        public UserService(IJwtProvider jwtProvider, IUserRepository userRepository)
        {
            _jwtProvider = jwtProvider;
            _passwordHasher = new PasswordHasher<User>();
            _userRepository = userRepository;
        }

        public async Task<JsonWebToken> Login(LoginCommand command)
        {
            var user = new User(Guid.NewGuid().ToString(), "login", "hasloooooooo", "Lukasz", "Tomalczyk", "1");
            var passwordHashed = _passwordHasher.VerifyHashedPassword(user, user.Password, command.Password);
            if(passwordHashed == PasswordVerificationResult.Failed)
            {
                throw new Exception();
            }
           
            await Task.CompletedTask;

            return _jwtProvider.Create(user.Id, user.Role);
        }

        public async Task<string> RegisterUser(RegisterCommand command)
        {
           
            var user = new User(Guid.NewGuid().ToString(), command.Login, command.Password, command.FirstName, command.LastName, command.Role);

            var passwordHashed = _passwordHasher.HashPassword(user, command.Password);

            user = new User(Guid.NewGuid().ToString(), command.Login, passwordHashed, command.FirstName, command.LastName, command.Role);

            var result = _userRepository.Save(user);

            return await Task.FromResult(result);
        }
    }
}
