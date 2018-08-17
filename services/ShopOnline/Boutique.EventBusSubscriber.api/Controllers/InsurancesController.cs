using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Interface;

namespace Boutique.EventBusSubscriber.api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class InsurancesController : Controller
    {
        private readonly IEventBusServices _eventBusServices;

        public InsurancesController(IEventBusServices eventBusServices)
        {
            _eventBusServices = eventBusServices;
        }
        
        [HttpGet]
        public JsonResult Index()
        {
            var messages = _eventBusServices.Subscribe("User");
            return Json("test");
        }
    }
}