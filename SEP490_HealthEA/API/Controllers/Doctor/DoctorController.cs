using AutoMapper;
using Domain.Interfaces.IRepositories;
using Domain.Models.DAO.Doctor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Doctor
{
	[Route("api/[controller]")]
	[ApiController]
	public class DoctorController : ControllerBase
	{
		private readonly IDoctorRepository repository;
		private readonly IMapper mapper;

		public DoctorController(IDoctorRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		[HttpGet()]
		public async Task<IActionResult> GetAllDoctor([FromQuery] string query)
		{
			var doctors = await repository.GetAllDoctors(query);
			var result = mapper.Map<IList<DoctorDto>>(doctors);
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetDoctorByDoctorId(Guid id)
		{
			var doctor = await repository.GetDoctorByIdAsync(id);
			if (doctor == null)
			{
				return NotFound();
			}
			var result = mapper.Map<DoctorDto>(doctor);
			return Ok(result);
		}


	}
}
