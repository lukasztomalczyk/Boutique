using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boutique.Infrastructure.Auth;
using Boutique.Infrastructure.CQRS.Commands;
using Boutique.Presentation.Commands;
using Boutique.Presentation.Commands.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Bountique.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IJwtProvider _jwtProvider;

        public ProductsController(ICommandDispatcher commandDispatcher, IJwtProvider jwtProvider)
        {
            _commandDispatcher = commandDispatcher;
            _jwtProvider = jwtProvider;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public string Load(LoadProductsCommand command)
        {
            var result = _commandDispatcher.Run<LoadProductsCommand, string>(command);
            return result;
        }

        [HttpPost]
        public JsonWebToken Login(LoginCommand token)
        {
            return _jwtProvider.Create("123456", "Admin"); 
        }

    }
}
