using Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Guest
{
    [Route("api/guest/[controller]")]
    [ApiController]
    public class HealthProfileController : BaseController
    {
        public IMedicalRecordsService _medicalRecordsServices;

        public HealthProfileController(IMedicalRecordsService medicalRecordsServices)
        {
            _medicalRecordsServices = medicalRecordsServices;
        }
        [HttpGet("infomation/{id}")]
        public IActionResult getInfoMDDetail(Guid id)
        {
            var s = _medicalRecordsServices.GetCommonInfoHealProfileById(User, id);
            return Ok(s);
        }
    }
}
