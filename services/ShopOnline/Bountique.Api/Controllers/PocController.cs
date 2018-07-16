using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace Bountique.Api.Controllers
{
    [Route("api/poc")]
    public class PocController : Controller
    {
        [HttpGet]
        public string Index()
        {
            return "hello wrold";
        }
    }
}