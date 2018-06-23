using System;
using System.Collections.Generic;
using System.Text;

namespace Boutique.Infrastructure.CQRS.Commands
{
    public interface ICommand
    {
    }

    public interface ICommandHandler<TCommand> : ICommand
    {
        void Run(TCommand command);
    }

    public interface ICommandHandler<TCommand, out TOut> : ICommand
    {
        TOut Handle(TCommand command);
    }

    public interface ICommandDispatcher
    {
        void Run<TCommand>(TCommand command);
        TOut Run<TCommand, TOut>(TCommand command);
    }
}
