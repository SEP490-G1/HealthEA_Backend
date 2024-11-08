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
			await repository.AddUserReportAsync(report);
			return CreatedAtAction(nameof(GetReportById), new { id = report.Id }, report);
		}
	}
}
