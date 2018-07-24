using Boutique.Infrastructure.Auth;
using Boutique.Infrastructure.CQRS.Commands;
using Boutique.Presentation.Commands.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bountique.Api.Controllers
{
    [Route("api/[controller]/[action]")]

    public class UsersController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public UsersController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        [AllowAnonymous]
        public string Register([FromBody]RegisterCommand registerCommand)
        {
            var result = _commandDispatcher.Run<RegisterCommand, string>(registerCommand);
            return result;
        }

        [HttpPost]
        [Authorize]
        //[Auth]
        public JsonWebToken Login([FromBody]LoginCommand command)
        {
            var result = _commandDispatcher.Run<LoginCommand, JsonWebToken>(command);
            return result;
        }
    }
}