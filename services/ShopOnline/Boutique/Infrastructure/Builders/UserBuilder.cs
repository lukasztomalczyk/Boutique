using Boutique.Domain.Users;
using Boutique.Infrastructure.DDD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boutique.Infrastructure.Builders
{
    public class UserBuilder
    {
        public string _id;
        public string _name;
        public string _lastName;
        public string _login;
        public string _password;
        public string _role;
        public IDomainEventDispatcher _domainEventDispatcher;


        public UserBuilder WithId(string id)
        {
            _id = id;
            return this;
        }

        public UserBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public UserBuilder WithLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }

        public UserBuilder WithLogin(string login)
        {
            _login = login;
            return this;
        }

        public UserBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public UserBuilder WithRole(string role)
        {
            _role = role;
            return this;
        }

        public UserBuilder WithEventDispatcher(IDomainEventDispatcher domainEventDispatcher)
        {
            _domainEventDispatcher = domainEventDispatcher;
            return this;
        }

        public User Create()
        {
            return new User(_id, _login, _password, _lastName, _lastName, _role, _domainEventDispatcher);
        }
    }
}
