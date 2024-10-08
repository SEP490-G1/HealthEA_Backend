using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Guest
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthProfileController : BaseController
    {
        [HttpGet]
        public IActionResult getProfile()
        {
            return Ok("Testing guest");
        }
    }
}
