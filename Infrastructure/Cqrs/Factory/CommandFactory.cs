using System;
using System.Collections.Generic;
using System.Text;
using Cqrs.Command;
using Microsoft.Extensions.DependencyInjection;

namespace Cqrs.Factory
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommandHandler<T> Create<T>()
        {
            return _serviceProvider.GetService<ICommandHandler<T>>();
        }

        public ICommandHandler<T, Result> Create<T, Result>()
        {
            return _serviceProvider.GetService<ICommandHandler<T,Result>>();
        }
    }
}
