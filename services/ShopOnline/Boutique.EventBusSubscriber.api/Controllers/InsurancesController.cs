using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Interface;

namespace Boutique.EventBusSubscriber.api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class InsurancesController : Controller
    {
        private readonly IRabbitMQReadClient _readClient;

        public InsurancesController(IRabbitMQReadClient readClient)
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