using Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Customer
{
    [Authorize(Roles = "User")]
    [Route("api/User/[controller]")]
    [ApiController]
    public class HealthProfileController : BaseController
    {
        public IMedicalRecordsService _medicalRecordsServices;

        public HealthProfileController(IMedicalRecordsService medicalRecordsServices)
        {
            _medicalRecordsServices = medicalRecordsServices;
        }
        [HttpGet]
        public IActionResult getAllHealProfile()
        {
            var s = _medicalRecordsServices.GetAllUsername(User);
            return Ok(s);
        }
    }
}
