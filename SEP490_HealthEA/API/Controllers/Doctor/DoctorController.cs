using AutoMapper;
using Domain.Interfaces.IRepositories;
using Domain.Interfaces.IServices;
using Domain.Models.DAO.Doctor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Doctor
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class DoctorController : ControllerBase
	{
		private readonly IDoctorRepository repository;
		private readonly IUserRepository userRepository;
		private readonly IUserClaimsService service;
		private readonly IMapper mapper;

		public DoctorController(IDoctorRepository repository, IMapper mapper, IUserClaimsService service, IUserRepository userRepository)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.service = service;
			this.userRepository = userRepository;
		}

		[HttpGet()]
		[AllowAnonymous]
		public async Task<IActionResult> GetAllDoctor([FromQuery] string? name, [FromQuery] string? city, [FromQuery] bool? getAll)
		{
			var doctors = await repository.GetAllDoctors(name, city, getAll);
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

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteDoctorById(Guid id)
		{
			var doctor = await repository.GetDoctorByIdAsync(id);
			if (doctor == null)
			{
				return BadRequest();
			}
			await repository.DeleteDoctorAsync(id);
			return NoContent();
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> UpdateDoctorById(Guid id, [FromBody] DoctorUpdateDto model)
		{
			var doctor = await repository.GetDoctorByIdAsync(id);
			if (doctor == null)
			{
				return BadRequest();
			}
			doctor.DisplayName = model.DisplayName;
			doctor.Description = model.Description;
			doctor.ClinicCity = model.ClinicCity;
			doctor.ClinicAddress = model.ClinicAddress;
			doctor.Specialization = model.Specialization;
			doctor.HistoryOfWork = model.HistoryOfWork;
			await repository.UpdateDoctorAsync(doctor);
			return NoContent();
		}

		[HttpGet("me")]
		public async Task<IActionResult> GetSelf()
		{
			var id = service.ClaimId(User);
			var doctor = await repository.GetDoctorByUserIdAsync(id);
			if (doctor == null)
			{
				return BadRequest("This user is not a doctor!");
			}
			var result = mapper.Map<DoctorDto>(doctor);
			return Ok(result);
		}

		[HttpPut("me")]
		public async Task<IActionResult> UpdateSelf(DoctorUpdateDto model)
		{
			var id = service.ClaimId(User);
			var doctor = await repository.GetDoctorByUserIdAsync(id);
			if (doctor == null)
			{
				return BadRequest("This user is not a doctor!");
			}
			doctor.DisplayName = model.DisplayName;
			doctor.Description = model.Description;
			doctor.ClinicCity = model.ClinicCity;
			doctor.ClinicAddress = model.ClinicAddress;
			doctor.Specialization = model.Specialization;
			doctor.HistoryOfWork = model.HistoryOfWork;
			await repository.UpdateDoctorAsync(doctor);
			return NoContent();
		}
	}
}
