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
        [HttpGet("{id}/{type}")]
        public IActionResult getListDocumentProfile(Guid id, int type)
        {
            return Ok();
        }
        /// <summary>
        /// get document profile by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetDocumentProfilebyId(Guid id)
        {
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteDocumentProfileById(Guid id)
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
        [HttpPut]
        public IActionResult UpdateDocumentProfile(Guid id, DocumentProfileInputDAO doc)
        {
            return Ok();
        }
    }
}
