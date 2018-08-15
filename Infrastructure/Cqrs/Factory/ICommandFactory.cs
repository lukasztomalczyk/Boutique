using System;
using System.Collections.Generic;
using System.Text;
using Cqrs.Command;

namespace Cqrs.Factory
{
    public interface ICommandFactory
    {
        ICommandHandler<TCommand> Create<TCommand>();

        ICommandHandler<TCommand, TCommandResult> Create<TCommand, TCommandResult>();
    }
}
