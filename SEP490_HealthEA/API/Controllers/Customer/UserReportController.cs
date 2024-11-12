using AutoMapper;
using Domain.Interfaces.IRepositories;
using Domain.Interfaces.IServices;
using Domain.Models.DAO.Doctor;
using Domain.Models.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Customer
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserReportController : ControllerBase
	{
		private readonly IUserReportRepository repository;
		private readonly IMapper mapper;
		private readonly IUserClaimsService userClaimsService;

		public UserReportController(IMapper mapper, IUserReportRepository repository, IUserClaimsService userClaimsService)
		{
			this.mapper = mapper;
			this.repository = repository;
			this.userClaimsService = userClaimsService;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetReportById(Guid id)
		{
			var report = await repository.GetReportByIdAsync(id);
			if (report == null)
				return NotFound();
			var model = mapper.Map<UserReportDto>(report);
			return Ok(model);
		}

		[HttpPost]
		public async Task<IActionResult> CreateReport([FromBody] UserReportAddDto model)
		{
			var report = mapper.Map<UserReport>(model);
			report.Id = Guid.NewGuid();
			report.ReporterId = userClaimsService.ClaimId(User);
			report.Status = 0;
			report.CreatedAt = DateTime.UtcNow;
			Console.WriteLine(report.ReporterId);
			await repository.AddUserReportAsync(report);
			return CreatedAtAction(nameof(GetReportById), new { id = report.Id }, report);
		}

		// GET: api/UserReport?status=0
		[HttpGet]
		public async Task<IActionResult> GetAllReports([FromQuery] int? status)
		{
			var reports = await repository.GetAllReportsAsync(status);
			var result = mapper.Map<IEnumerable<UserReportDto>>(reports);
			return Ok(result);
		}

		// PATCH: api/UserReport/{id}/status
		[HttpPatch("{id}/status")]
		public async Task<IActionResult> UpdateReportStatus(Guid id, [FromBody] ReportStatusDto model)
		{
			await repository.MarkReportStatusAsync(id, model.Status);
			return NoContent();
		}
	}

	public class ReportStatusDto
	{
		public int Status { get; set; }
	}
}
