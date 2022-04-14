using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TM.TestService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        public HealthController()
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
