using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : Controller
    {
        [HttpGet]
        public IActionResult getAll()
        {
            throw new Exception("New Êcpt");
        }
    }
}
