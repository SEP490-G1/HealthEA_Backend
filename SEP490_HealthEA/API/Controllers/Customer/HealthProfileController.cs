using Domain.Interfaces.IServices;
using Domain.Models;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Customer
{
    [Authorize(Roles = "User")]
    [Route("api/user/[controller]")]
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
            var s = _medicalRecordsServices.GetAllHealProfileByToken(User);
            return Ok(s);
        }
        [HttpGet("info/{id}")]
        public IActionResult getInfoMDDetail(Guid id)
        {
            var s = _medicalRecordsServices.GetCommonInfoHealProfileById(User, id);
            return Ok(s);
        }
        [HttpPost]
        public IActionResult addNewHealthProfile([FromBody] HealthProfileInput profile)
        {
            var serviceResult = _medicalRecordsServices.AddNewHealthProfile(User, profile);
            return Ok(serviceResult);
        }
        [HttpPatch ("{id}")]
        public IActionResult updateHealthProfile([FromBody]HealthProfile profile)
        {
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult removeHealthProfile(Guid id)
        {
            return Ok();
        }
        [HttpPatch ("share/{id}")]
        public IActionResult updateShareHealthProfile([FromBody] int stone)
        {
            return Ok();
        }
        
    }
}
