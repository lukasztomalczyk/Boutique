using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Interface;

namespace Boutique.EventBusSubscriber.api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class InsurancesController : Controller
    {
        private readonly IRabbitMqReadClient _readClient;
        private readonly IRabbitMqWriteClient _writeClient;

        public InsurancesController(IRabbitMqReadClient readClient, IRabbitMqWriteClient writeClient)
        {
            _readClient = readClient;
            _writeClient = writeClient;
        }
        
        [HttpGet]
        public string Read()
        {

                return _readClient.Read("Insurances");
        }

        [HttpGet("{queue}/{message}")]
        public string Write(string message, string @queue)
        {
           return  $"{@queue} : {message}";
        }
    }
}