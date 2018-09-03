using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Interface;

namespace Boutique.EventBusSubscriber.api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class InsurancesController : Controller
    {
        private readonly IRabbitMqReadClient _readClient;

        public InsurancesController(IRabbitMqReadClient readClient)
        {
            _readClient = readClient;
        }
        
        [HttpGet]
        public string Index()
        {
            var messages = _readClient.Read("User");
            return messages;
        }
    }
}