using System;
using System.Collections.Generic;
using System.Text;

namespace Boutique.Domain.Users
{
    public class User
    {
        public string Id { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Role { get; private set; }
       // private IDomainEventDispatcher _domainEventDispatcher;

        private User() { }

        public User(string id, string login, string password, string firstName, string lastName, string role)
        {
            Id = id;
            Password = password;
            Role = role;
            LastName = lastName;
            FirstName = firstName;
            Login = login;
      }
    }
}
