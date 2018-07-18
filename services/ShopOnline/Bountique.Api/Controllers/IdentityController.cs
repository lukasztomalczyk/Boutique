using System;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace Bountique.Api.Controllers
{
    [Route("api/{controller}")]
    public class IdentityController : Controller
    {
        [Route("Index")]
        public async Task<JsonResult> Index()
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5001");
            if (disco.IsError)
            {
                return Json(disco.Error);

            }
            
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            if (tokenResponse.IsError)
            {
                return Json(tokenResponse.Error);
            }

            return Json(tokenResponse.Json);
            
        }
    }
}