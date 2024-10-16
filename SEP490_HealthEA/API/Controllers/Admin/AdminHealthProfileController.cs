using Domain.Interfaces.IServices;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("api/Admin/HealthProfile")]
    [ApiController]
    public class AdminHealthProfileController : BaseController
    {
        public IMedicalRecordsService _medicalRecordsServices;

        public AdminHealthProfileController(IMedicalRecordsService medicalRecordsServices)
        {
            _medicalRecordsServices = medicalRecordsServices;
        }
        [HttpGet]
        public IActionResult getAllHealProfile()
        {
            return Ok("oke admin");
        }
    }
}
