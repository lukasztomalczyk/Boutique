using System;
using System.Collections.Generic;
using System.Text;
using Cqrs.Command;

namespace Boutique.Presentation.Commands.Auth
{
    public class LoginCommand : ICommand
    {
        public LoginCommand(string username, string password)
        {
            UserName = username;
            Password = password;
        }

        public string UserName { get; }
        public string Password { get; }
    }
}
