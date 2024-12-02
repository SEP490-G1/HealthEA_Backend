using AutoMapper;
using Domain.Interfaces.IRepositories;
using Domain.Interfaces.IServices;
using Domain.Models.DAO.Doctor;
using Domain.Models.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.SQLServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Customer
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class UserReportController : ControllerBase
	{
		private readonly IUserReportRepository repository;
		private readonly IMapper mapper;
		private readonly IUserClaimsService userClaimsService;
		private readonly EmailService emailService;

		private readonly IDoctorRepository doctorRepository;
		private readonly INewsRepository newsRepository;
		private readonly IUserRepository userRepository;

		public UserReportController(IMapper mapper, IUserReportRepository repository, IUserClaimsService userClaimsService, IDoctorRepository doctorRepository, INewsRepository newsRepository, EmailService emailService, IUserRepository userRepository)
		{
			this.mapper = mapper;
			this.repository = repository;
			this.userClaimsService = userClaimsService;
			this.doctorRepository = doctorRepository;
			this.newsRepository = newsRepository;
			this.emailService = emailService;
			this.userRepository = userRepository;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetReportById(Guid id)
		{
			var report = await repository.GetReportByIdAsync(id);
			if (report == null)
				return NotFound();
			var model = mapper.Map<UserReportDto>(report);
			//Get type
			if (model.ReportType == "doctor")
			{
				var obj = await doctorRepository.GetDoctorByIdAsync(model.ReportedId);
				if (obj != null)
				{
					model.Reported = new ReportedObjectDto();
					model.Reported.Name = obj.DisplayName;
				}
			} else if (model.ReportType == "news")
			{
				var obj = await newsRepository.GetByIdAsync(model.ReportedId);
				if (obj != null)
				{
					model.Reported = new ReportedObjectDto();
					model.Reported.Name = obj.Title;
				}
			}
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
			//Send email to user
			var user = await userRepository.GetUserByIdAsync(report.ReporterId);
			if (user != null)
			{
				var thread = new Thread(async () =>
				{
					await emailService.SendEmailAsync(user.Email,
					"Chúng tôi đã nhận được báo cáo của bạn",
					$"<h2>Xin chào {user.Username}.</h2>" +
					"<p>Chúng tôi đã nhận được báo cáo của bạn và sẽ tiến hành xem xét trong thời gian sớm nhất.</p>" +
					"<p>Cảm ơn bạn đã giúp chúng tôi cải thiện hệ thống.</p>");
				});
				thread.Start();
			}
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
			if (model.Status == 1)
			{
				UserReport report = await repository.GetReportByIdAsync(id);
				if (report != null)
				{
					var user = await userRepository.GetUserByIdAsync(report.ReporterId);
					if (user != null)
					{
						var thread = new Thread(async () =>
						{
							await emailService.SendEmailAsync(user.Email,
							"Báo cáo của bạn đã được giải quyết",
							$"<h2>Xin chào {user.Username}.</h2>" +
							"<p>Chúng tôi đã xem xét báo cáo của bạn và đã xử lý vấn đề. Nếu có thắc mắc hoặc cần hỗ trợ thêm, vui lòng liên hệ lại với chúng tôi.</p>" +
							"<p>Cảm ơn bạn đã giúp chúng tôi cải thiện hệ thống.</p>");
						});
						thread.Start();
					}
				}
			}
			return NoContent();
		}
	}

	public class ReportStatusDto
	{
		public int Status { get; set; }
	}
}
