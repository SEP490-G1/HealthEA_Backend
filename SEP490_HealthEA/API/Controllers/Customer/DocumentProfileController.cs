using Domain.Interfaces.IRepositories;
using Domain.Interfaces.IServices;
using Domain.Models.DAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Customer
{

    [Authorize(Roles = "CUSTOMER")]
    [Route("api/customer/[controller]")]
    [ApiController]
    public class DocumentProfileController : ControllerBase
    {
        public IMedicalRecordsService _medicalRecordsServices;
        public IMedicalRecordRepository _repository;
        public DocumentProfileController(IMedicalRecordsService medicalRecordsServices, IMedicalRecordRepository repository)
        {
            _medicalRecordsServices = medicalRecordsServices;
            _repository = repository;
        }

        //get a docmuent profile record of o type 
        [HttpGet("{id}")]
        public IActionResult getDocumentProfile(Guid id, int type)
        {
            return Ok();
        }


        //create a healthProfile a type
        [HttpPost]
        public IActionResult createDocumentProfile(DocumentProfileInputDAO doc)
        {
            var s = _medicalRecordsServices.createDocumentProfile(User, doc);
            return Ok(s);
        }
        //edit a medical record 
        //remove a medical record 
    }
}
