using Aplication_A.BL.Interface;
using Aplication_A.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Aplication_A.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RabbitMqSendController : ControllerBase
    {
        
        private readonly ILogger<RabbitMqSendController> _logger;
        private readonly IRabbitMqService _rabbitMqService;

        public RabbitMqSendController(ILogger<RabbitMqSendController> logger, IRabbitMqService rabbitMqService)
        {
            _logger = logger;
            _rabbitMqService = rabbitMqService;
        }

        [HttpPost]
        public async Task<IActionResult> SendPerson ([FromBody] Person p)
        {
            await _rabbitMqService.SendPersonAsync(p);

            return Ok();
        }
    }
}
