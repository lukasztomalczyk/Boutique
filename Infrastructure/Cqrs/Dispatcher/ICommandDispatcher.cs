using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cqrs.Dispatcher
{
    public interface ICommandDispatcher
    {
        void Dispatch<TCommand>(TCommand command);
        TCommandResult Dispatch<TCommand,TCommandResult>(TCommand command);

        Task DispatchAsync<TCommand>(TCommand command);
        Task<TCommandResult> DispatchAsync<TCommand, TCommandResult>(TCommand command);
    }
}
