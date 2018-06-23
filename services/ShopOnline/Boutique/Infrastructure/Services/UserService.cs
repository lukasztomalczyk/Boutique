using Boutique.Domain.Users;
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
    public class UserService : IUserService
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher<User> _passwordHasher;

        /// 1. Repository dla user
        /// 2. User isExits
        /// 3. Password Comapre
        /// 

        public UserService(IJwtProvider jwtProvider, IPasswordHasher<User> passwordHasher)
        {
            _jwtProvider = jwtProvider;
            _passwordHasher = passwordHasher;
        }

        public async Task<JsonWebToken> Login(LoginCommand command)
        {
            var user = new User("222", "login", "hasloooooooo", "Lukasz", "Tomalczyk", "1");
            var passwordHashed = _passwordHasher.VerifyHashedPassword(user, user.Password, command.Password);
            if(passwordHashed == PasswordVerificationResult.Failed)
            {
                throw new Exception();
            }

            await Task.CompletedTask;

            return _jwtProvider.Create(user.Id, user.Role);
        }

        public Task RegisterUser(RegisterCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
