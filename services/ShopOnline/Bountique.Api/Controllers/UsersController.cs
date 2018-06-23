using Boutique.Application.CommandHandler;
using Boutique.Infrastructure.Auth;
using Boutique.Infrastructure.CQRS.Commands;
using Boutique.Presentation.Commands.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Bountique.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public UsersController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public string Register(RegisterCommand registerCommand)
        {
            var result = _commandDispatcher.Run<RegisterCommand, string>(registerCommand);
            return result;
        }

        [HttpPost]
        [Auth]
        public string Login(LoginCommand command)
        {
            var result = _commandDispatcher.Run<LoginCommand, string>(Login);
            return result;
        }
    }
}