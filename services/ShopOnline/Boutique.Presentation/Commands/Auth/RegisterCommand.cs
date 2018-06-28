using System;
using System.Collections.Generic;
using System.Text;

namespace Boutique.Presentation.Commands.Auth
{
    public class RegisterCommand
    {
        public string Login { get; }
        public string Password { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Role { get; }

        public RegisterCommand(string login, string password, string firstName, string lastName, string role)
        {
            Login = login;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }

    }
}
