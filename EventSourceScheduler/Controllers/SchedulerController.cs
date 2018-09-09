using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Interface;

namespace EventSourceScheduler.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SchedulerController : ControllerBase
    {
        private readonly IRabbitMqReadClient _rabbitMqReadClient;

        public SchedulerController(IRabbitMqReadClient rabbitMqReadClient)
        {
            _rabbitMqReadClient = rabbitMqReadClient;
        }

        [HttpGet]
        public string Read()
        {
           return _rabbitMqReadClient.Read("Insurances");
        }
    }
}
