using Microsoft.AspNetCore.Mvc;

namespace GateWay.Controllers
{
    [ApiController]
    [Route("/")]
    public class RootController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { status = "Gateway is running 🚀" });
        }
    }
}
