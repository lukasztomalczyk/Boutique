using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace Boutique.Infrastructure.CQRS.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly SqlTransaction _sqlTransaction;
        public CommandDispatcher(IServiceProvider serviceProvider, SqlTransaction sqlTransaction)
        {
            _serviceProvider = serviceProvider;
            _sqlTransaction = sqlTransaction;
        }

        public void Run<TCommand>(TCommand command)
        {
            var handler = _serviceProvider.GetService<IDomainCommandHandler<TCommand>>();

            if (handler == null)
                throw new ArgumentException($"Executed event type {command} doest'n exists in scope services.");
            using (_sqlTransaction)
            {
                try
                {
                    handler.Run(command);
                    _sqlTransaction.Commit();
                }
                catch (Exception)
                {
                    _sqlTransaction.Rollback();
                    throw;
                }
            }
        }

        public TOut Run<TCommand, TOut>(TCommand command)
        {
            var handler = _serviceProvider.GetService<IDomainCommandHandler<TCommand, TOut>>();

            if (handler == null)
                throw new ArgumentException($"Executed event type {command} doest'n exists in scope services.");
            using (_sqlTransaction)
            {
                try
                {
                    var result = handler.Handle(command);
                    _sqlTransaction.Commit();

                    return result;
                }
                catch (Exception)
                {
                    _sqlTransaction.Rollback();
                    throw;
                }
            }
        }
    }
}
