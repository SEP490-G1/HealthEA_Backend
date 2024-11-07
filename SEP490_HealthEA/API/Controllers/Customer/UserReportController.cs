using AutoMapper;
using Domain.Interfaces.IRepositories;
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

		public UserReportController(IMapper mapper, IUserReportRepository repository)
		{
			this.mapper = mapper;
			this.repository = repository;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetReportById(Guid id)
		{
			var report = await repository.GetReportByIdAsync(id);
			if (report == null)
				return NotFound();

			return Ok(report);
		}

		[HttpPost]
		public async Task<IActionResult> CreateReport([FromBody] UserReport report)
		{
			if (report == null || report.ReporterId == Guid.Empty)
				return BadRequest("Report information is incomplete.");

			report.Id = Guid.NewGuid();
			report.Status = 0;
			report.CreatedAt = DateTime.UtcNow;

			await repository.AddUserReportAsync(report);
			return CreatedAtAction(nameof(GetReportById), new { id = report.Id }, report);
		}
	}
}
