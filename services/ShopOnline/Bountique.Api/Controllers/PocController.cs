using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Bountique.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PocController : Controller
    {
        [HttpGet]
        [Authorize]
        public string Index()
        {
            return "hello wroldddddddd";
        }
    }
}