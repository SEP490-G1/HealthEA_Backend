
using Domain.Interfaces.IServices;
using Domain.Models.DAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Customer
{

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
        public IActionResult getListDocumentProfile(Guid idHealprofile, int typeDocuemntProfile)
        {
            var serviceResult = _service.GetListDocumentProfile(User, idHealprofile, typeDocuemntProfile);
            return Ok(serviceResult);
        }

        [HttpGet("{id}")]
        public IActionResult GetDocumentProfileDetailbyId(Guid id)
        {
            var res = _service.GetDocumentProfileDetail(User, id);
            return Ok(res);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteDocumentProfileById(Guid id)
        {
            var res = _service.DeleteDocumentProfileByid(User, id);
            return Ok(res);
        }

        //create a healthProfile a type
        [Authorize(Roles = "CUSTOMER")]
        [HttpPost]
        public IActionResult createDocumentProfile(DocumentProfileInputDAO doc)
        {
            var s = _service.createDocumentProfile(User, doc);
            return Ok(s);
        }
        //edit a medical record
        [Authorize]
        [HttpPut]
        public IActionResult UpdateDocumentProfile(Guid id, DocumentProfileInputDAO doc)
        {
            var s = _service.UpdateDocumentProfile(User, id, doc);
            return Ok(s);
        }
    }
}
