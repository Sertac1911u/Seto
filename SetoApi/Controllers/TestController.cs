using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SetoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [Authorize]
        [HttpGet("Private")]
        public IActionResult Private()
        {
            return Ok(new { Message = "Oldu" });
        }
    }
}
