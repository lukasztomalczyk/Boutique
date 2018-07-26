using System;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Boutique.Domain;
using Boutique.Domain.Users;
using Boutique.Infrastructure.Attributes;
using Boutique.Infrastructure.Auth;
using Boutique.Infrastructure.Settings;
using Boutique.Presentation.Commands.Auth;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Boutique.Infrastructure.Services.Password;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Boutique.Infrastructure.Services
{
    [Services]
    public class UserService : IUserService
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;

        /// 1. Repository dla user
        /// 2. User isExits
        /// 3. Password Comapre
        /// 

        public UserService(IJwtProvider jwtProvider, IUserRepository userRepository)
        {
            _jwtProvider = jwtProvider;
            _passwordHasher = new PasswordHasher();
            _userRepository = userRepository;
         }

//        public async Task<JsonWebToken> Login(LoginCommand command)
//        {
//            var user = new User(Guid.NewGuid().ToString(), "login", "hasloooooooo", "Lukasz", "Tomalczyk", "1");
//            var passwordHashed = _passwordHasher.VerifyHashedPassword(user, user.Password, command.Password);
//            if (passwordHashed == PasswordVerificationResult.Failed)
//            {
//                throw new Exception();
//            }
//
//            await Task.CompletedTask;
//
//            return _jwtProvider.Create(user.Id, user.Role);
//        }

        public async Task<JsonWebToken> Login(LoginCommand command)
        {
            User user = _userRepository.Load(command.UserName);
            
            var passwordHashed = _passwordHasher.VerifyHashedPassword(user.Password, command.Password);

            if (user != null && user.Password == command.Password)
            {
                return await Task.FromResult(_jwtProvider.Create(user.Login, user.Id, user.Role));
            }

            return await Task.FromException<JsonWebToken>(new InvalidOperationException("invalid user"));
        }

        public async Task<string> RegisterUser(RegisterCommand command)
        {
            var passwordHashed = _passwordHasher.HashPassword(command.Password);

            var user = new User(Guid.NewGuid().ToString(), command.Login, passwordHashed, command.FirstName,
                command.LastName, command.Role);

            var result = _userRepository.Save(user);

            return await Task.FromResult(result);
        }

        public bool IsUserExists(string userName)
        {
            return  _userRepository.Contains(userName);
        }

        public Task<bool> ValidateUserCredentialAsync(string userName, string userPassword)
        {
            var isValid = (_passwordHasher.VerifyHashedPassword(userName, userPassword) && IsUserExists(userName));
            return Task.FromResult(isValid);
        }

        public async void SingIn(HttpContext context)
        {
          // context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(_jwtProvider.))
        }
        
    }

}
