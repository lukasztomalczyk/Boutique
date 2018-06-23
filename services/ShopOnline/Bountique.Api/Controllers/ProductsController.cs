using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boutique.Infrastructure.Auth;
using Boutique.Infrastructure.CQRS.Commands;
using Boutique.Presentation.Commands;
using Boutique.Presentation.Commands.Auth;
using Boutique.Presentation.Commands.Products;
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
        [Auth]
        public string Load(LoadProductsCommand command)
        {
            var result = _commandDispatcher.Run<LoadProductsCommand, string>(command);
            return result;
        }

        [HttpPost]
        [Auth("admin")]
        public IActionResult Add(CreateProductCommand command)
        {
            return Created(nameof(Load), null);
        }

    }
}
