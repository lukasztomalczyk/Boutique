using Cqrs.Dispatcher;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cqrs.Decorators
{
    public class NLogCommandDecorator : ICommandGateway
    {
        private readonly ICommandGateway _commandGateway;
        private readonly ILogger _logger;

        public NLogCommandDecorator(ICommandGateway commandGateway, ILoggerFactory loggerFactory)
        {
            _commandGateway = commandGateway;
            _logger = loggerFactory.CreateLogger<NLogCommandDecorator>();
        }

        public void Call<TCommand>(TCommand command)
        {
            using (_logger.BeginScope(command))
                try
                {
                    _logger.LogInformation($"Invoking command {typeof(TCommand)} with arguments {JsonConvert.SerializeObject(command)}");
                    _commandGateway.Call(command);
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex, ex.Message);
                    throw;
                }
        }

        public TCommandResult Call<TCommand, TCommandResult>(TCommand command)
        {
            using (_logger.BeginScope(command))
                try
                {
                    _logger.LogInformation($"Invoking command {typeof(TCommand)} with arguments {JsonConvert.SerializeObject(command)}");
                    return _commandGateway.Call<TCommand, TCommandResult>(command);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    throw;
                }
        }

        public async Task CallAsync<TCommand>(TCommand command)
        {
            using (_logger.BeginScope(command))
                try
                {
                    _logger.LogInformation($"Invoking command {typeof(TCommand)} with arguments {JsonConvert.SerializeObject(command)}");
                    await Task.Run(() => _commandGateway.CallAsync(command));
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex, ex.Message);
                    throw;
                }
        }

        public async Task<TCommandResult> CallAsync<TCommand, TCommandResult>(TCommand command)
        {
            using (_logger.BeginScope(command))
                try
                {
                    _logger.LogInformation($"Invoking command {typeof(TCommand)} with arguments {JsonConvert.SerializeObject(command)}");
                    return await Task.Run(() => _commandGateway.CallAsync<TCommand, TCommandResult>(command));
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Invoking command {typeof(TCommand)} with arguments {JsonConvert.SerializeObject(command)}");
                    _logger.LogError(ex, ex.Message);
                    throw;
                }
        }
    }
}
