using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cqrs
{
    public interface ICommandGateway
    {
        void Call<TCommand>(TCommand command);
        Task CallAsync<TCommand>(TCommand command);

        TCommandResult Call<TCommand,TCommandResult>(TCommand command);
        Task<TCommandResult> CallAsync<TCommand, TCommandResult>(TCommand command);
    }
}
