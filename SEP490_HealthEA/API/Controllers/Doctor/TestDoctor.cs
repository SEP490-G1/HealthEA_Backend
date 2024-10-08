using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Doctor
{

    [Authorize(Roles = "Doctor")]
    [Route("api/Doctor/[controller]")]
    [ApiController]
    public class TestDoctor : BaseController
    {
        [HttpGet]
        public IActionResult getAllHealProfile()
        {
            return Ok("Api này để test role doctor");
        }
    }
}
