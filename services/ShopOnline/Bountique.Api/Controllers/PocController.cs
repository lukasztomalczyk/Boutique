using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Bountique.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PocController : Controller
    {
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public string Index()
        {
            return "hello wroldddddddd";
        }

        [HttpPost]
        public dynamic Token(HttpContext context)
        {
            var test = context.User.Claims.ToList();
            return test;
        }
    }
}