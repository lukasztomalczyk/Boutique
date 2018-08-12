using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cqrs.Handlers
{
    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<TCommand> : ICommandHandler
    {
        Task HandleAsync(TCommand command);
        void Handle(TCommand command);
    }
    public interface ICommandHandler<TCommand, TCommandResult> : ICommandHandler
    {
        TCommandResult Handle(TCommand command);
        Task<TCommandResult> HandleAsync(TCommand command);
    }
}
