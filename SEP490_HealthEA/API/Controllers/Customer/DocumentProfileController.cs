
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
        public IMedicalRecordsService _service;
        public DocumentProfileController(IMedicalRecordsService medicalRecordsServices
            )
        {
            _service = medicalRecordsServices;

        }

        //get a docmuent profile record of o type 
        [HttpGet("{idHealprofile}/{typeDocuemntProfile}")]
        public IActionResult getListDocumentProfile(Guid idHealthprofile, int typeDocuemntProfile)
        {
            var serviceResult = _service.GetListDocumentProfile(User, idHealthprofile,  typeDocuemntProfile);
            return Ok(serviceResult);
        }
        /// <summary>
        /// get document profile by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetDocumentProfileDetailbyId(Guid id)
        {
            var res = _service.GetDocumentProfileDetail(User, id);
            return Ok(res);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteDocumentProfileById(Guid id)
        {
            var res = _service.DeleteDocumentProfileByid(User, id);
            return Ok(res);
        }

        //create a healthProfile a type
        [HttpPost]
        public IActionResult createDocumentProfile(DocumentProfileInputDAO doc)
        {
            var s = _service.createDocumentProfile(User, doc);
            return Ok(s);
        }
        //edit a medical record
        [HttpPut]
        public IActionResult UpdateDocumentProfile(Guid id, DocumentProfileInputDAO doc)
        {
            var s = _service.UpdateDocumentProfile(User, id, doc);
            return Ok(s);
        }
    }
}
