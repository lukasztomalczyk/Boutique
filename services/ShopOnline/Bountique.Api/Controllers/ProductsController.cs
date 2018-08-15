using System.Threading.Tasks;
using Boutique.Presentation.Commands;
using Cqrs;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Bountique.Api.Controllers
{
    [Route("api/[controller]/[action]")]

    public class ProductsController : Controller
    {
        private readonly ICommandGateway _gateway;

        public ProductsController(ICommandGateway gateway)
        {
            _gateway = gateway;
        }

//        [HttpPost]
//        //[Authorize(Policy = "Admin")]
//        public async Task<string> Load([FromBody] LoadProductsCommand command)
//        {
//            var result = await _gateway.CallAsync<LoadProductsCommand, string>(command);
//            return result;
//        }
//
//        [HttpPost]
//        //[Authorize(Policy = "Admin")]
//        public string Create([FromBody] CreateProductCommand command)
//        {
//            var result = _gateway.Call<CreateProductCommand, string>(command);
//            return result;
//        }

        
    }
}
