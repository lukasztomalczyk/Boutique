using Cqrs.Dispatcher;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cqrs
{
    public class CommandGateway : ICommandGateway
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public CommandGateway(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public void Call<TCommand>(TCommand command)
        {
            _commandDispatcher.Dispatch(command);
        }

        public async Task CallAsync<TCommand>(TCommand command)
        {
           await Task.Run( ()=> _commandDispatcher.DispatchAsync(command));
        }

        public TCommandResult Call<TCommand, TCommandResult>(TCommand command)
        {
            return _commandDispatcher.Dispatch<TCommand, TCommandResult>(command);
        }

        public async Task<TCommandResult> CallAsync<TCommand, TCommandResult>(TCommand command)
        {
            return await Task.Run(() => _commandDispatcher.DispatchAsync<TCommand, TCommandResult>(command));
        }

    }
}
