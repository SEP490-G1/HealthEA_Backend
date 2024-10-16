using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Doctor
{

    [Authorize(Roles = "Doctor")]
    [Route("api/doctor/[controller]")]
    [ApiController]
    public class HealthProfileController : BaseController
    {
      
    }
}
