using System;
using System.Threading.Tasks;
using Boutique.Domain.Users;
using IdentityModel.Client;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Mvc;

namespace Bountique.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class IdentityController : Controller
    {
        [HttpPost]
        public async Task<JsonResult> Index([FromBody] UserForTest user)
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5001");
            if (disco.IsError)
            {
                return Json(disco.Error);

            }
            var tokenClient = new TokenClient(disco.TokenEndpoint, user.ClientID, user.Secret);
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api");

            if (tokenResponse.IsError)
            {
                return Json(tokenResponse.Error);
            }

            return Json(tokenResponse.Json);
            
        }
    }
}