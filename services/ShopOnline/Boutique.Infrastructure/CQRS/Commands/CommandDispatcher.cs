using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace Boutique.Infrastructure.CQRS.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Run<TCommand>(TCommand command)
        {
            var handler = _serviceProvider.GetService<IDomainCommandHandler<TCommand>>();

            if (handler == null)
                throw new ArgumentException($"Executed event type {command} doest'n exists in scope services.");
            using (var trasaction = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                try
                {
                    handler.Run(command);
                    trasaction.Complete();
                }
                catch (Exception)
                {
                    trasaction.Dispose();
                    throw;
                }
            }

        }

        public TOut Run<TCommand, TOut>(TCommand command)
        {
            var handler = _serviceProvider.GetService<IDomainCommandHandler<TCommand, TOut>>();

            if (handler == null)
                throw new ArgumentException($"Executed event type {command} doest'n exists in scope services.");
            using (var trasaction = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                try
                {
                    var result = handler.Handle(command);
                    trasaction.Complete();

                    return result;
                }
                catch (Exception)
                {
                    trasaction.Dispose();
                    throw;
                }
            }
        }
    }
}
