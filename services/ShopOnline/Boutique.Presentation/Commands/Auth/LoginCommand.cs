using System;
using System.Collections.Generic;
using System.Text;

namespace Boutique.Presentation.Commands.Auth
{
    public class LoginCommand
    {
        public LoginCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }
        public string Password { get; }
    }
}
