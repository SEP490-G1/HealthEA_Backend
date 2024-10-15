using AutoMapper;
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

        
        [HttpPost]
        public IActionResult addNewHealthProfile([FromBody] HealthProfileInput profile)
        {
            var serviceResult = _medicalRecordsServices.AddNewHealthProfile(User, profile);
            return Ok(serviceResult);
        }
        [HttpPatch ("{id}")]
        public IActionResult updateHealthProfile([FromBody] HealthProfileInput profile)
        {
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult removeHealthProfile(Guid id)
        {
            var serviceResult = _medicalRecordsServices.RemoveHealthProfile(User, id);
            return Ok(serviceResult);
        }
        [HttpPatch ("share/{id}")]
        public IActionResult updateShareHealthProfile(Guid id, [FromBody] int stone)
        {

            var serviceResult = _medicalRecordsServices.UpdateShareHealthProfil(User, id, stone);
            return Ok(serviceResult);
        }
        
    }
}
