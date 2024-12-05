using AutoMapper;
using Domain.Interfaces.IServices;
using Domain.Models;
using Domain.Models.DAO;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Customer
{
    [Route("api/customer/[controller]")]
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

        [Authorize(Roles = "CUSTOMER")]
        [HttpGet]
        public IActionResult getAllHealProfile()
        {
            var s = _medicalRecordsServices.GetAllHealProfileByToken(User);
            return Ok(s);
        }
        [Authorize(Roles = "CUSTOMER")]

        [HttpPost]
        public IActionResult addNewHealthProfile([FromBody] HealthProfileInputDAO profile)
        {
            var serviceResult = _medicalRecordsServices.AddNewHealthProfile(User, profile);
            return Ok(serviceResult);
        }
        [Authorize(Roles = "CUSTOMER")]

        [HttpPatch("{id}")]
        public IActionResult updateHealthProfile(Guid id, [FromBody] HealthProfileInputDAO profile)
        {
            var serviceResult = _medicalRecordsServices.UpdateInfoHealthProfile(User, id, profile);
            return Ok(serviceResult);
        }
        [Authorize(Roles = "CUSTOMER")]

        [HttpDelete("{id}")]
        public IActionResult removeHealthProfile(Guid id)
        {
            var serviceResult = _medicalRecordsServices.RemoveHealthProfile(User, id);
            return Ok(serviceResult);
        }
        [Authorize(Roles = "CUSTOMER")]

        [HttpPatch("share/{id}")]
        public IActionResult updateShareHealthProfile(Guid id, [FromBody] int stone)
        {
            var serviceResult = _medicalRecordsServices.UpdateShareHealthProfile(User, id, stone);
            return Ok(serviceResult);
        }
    }
}
