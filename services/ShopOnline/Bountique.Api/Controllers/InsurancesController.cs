using System.Threading.Tasks;
using Boutique.Presentation.Commands;
using Boutique.Presentation.Commands.Insurance;
using Cqrs;
using Microsoft.AspNetCore.Mvc;

namespace Bountique.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class InsurancesController : Controller
    {
        private readonly ICommandGateway _gateway;

        public InsurancesController(ICommandGateway gateway)
        {
            _gateway = gateway;
        }
        
        [HttpPost]
        public async Task<string> Create([FromBody] CreateInsuranceCommand command)
        {
            var result = await _gateway.CallAsync<CreateInsuranceCommand, string>(command);
            return result;
        }
    }
}