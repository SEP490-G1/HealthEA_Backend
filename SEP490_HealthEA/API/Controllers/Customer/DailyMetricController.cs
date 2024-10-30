using AutoMapper;
using Domain.Interfaces.IRepositories;
using Domain.Interfaces.IServices;
using Domain.Models.DAO.DailyMetrics;
using Domain.Models.Entities.YourNamespace.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Customer
{
	[Route("api/[controller]")]
	[Authorize]
	[ApiController]
	public class DailyMetricController : ControllerBase
	{
		private readonly IDailyMetricRepository repository;
		private readonly IDailyMetricsAnalysisService service;
		private readonly IUserClaimsService userClaimsService;
		private readonly IMapper mapper;

		public DailyMetricController(IDailyMetricRepository repository, IDailyMetricsAnalysisService service, IUserClaimsService userClaimsService, IMapper mapper)
		{
			this.repository = repository;
			this.service = service;
			this.userClaimsService = userClaimsService;
			this.mapper = mapper;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<DailyMetric>> GetDailyMetricById(Guid id)
		{
			var dailyMetric = await repository.GetByIdAsync(id);
			if (dailyMetric == null)
			{
				return NotFound();
			}
			var result = mapper.Map<DailyMetricReturnModel>(dailyMetric);
			return Ok(result);
		}

		[HttpGet("me")]
		public async Task<ActionResult<IEnumerable<DailyMetric>>> GetDailyMetricsOfUser()
		{
			var userId = userClaimsService.ClaimId(User);
			var dailyMetrics = await repository.GetAllByUserIdAsync(userId);
			var result = mapper.Map<IEnumerable<DailyMetricReturnModel>>(dailyMetrics);
			return Ok(result);
		}

		[HttpPost]
		[Obsolete]
		public async Task<ActionResult> CreateDailyMetric([FromBody] DailyMetric dailyMetric)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			dailyMetric.Id = new Guid();
			dailyMetric.Date = DateOnly.FromDateTime(DateTime.Today);
			await repository.AddAsync(dailyMetric);
			return NoContent();
		}

		[HttpPut]
		[Obsolete]
		public async Task<IActionResult> UpdateDailyMetric([FromBody] DailyMetric dailyMetric)
		{
			var existingMetric = await repository.GetByIdAsync(dailyMetric.Id);
			if (existingMetric == null)
			{
				return NotFound();
			}
			dailyMetric.Date = DateOnly.FromDateTime(DateTime.Today);
			await repository.UpdateAsync(dailyMetric);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteDailyMetric(Guid id)
		{
			var dailyMetric = await repository.GetByIdAsync(id);
			if (dailyMetric == null)
			{
				return NotFound();
			}
			await repository.DeleteAsync(id);
			return NoContent();
		}

		[HttpGet("analyze/{id}")]
		public async Task<IActionResult> AnalyzeDailyMetric(Guid id)
		{
			var dailyMetric = await repository.GetByIdAsync(id);
			if (dailyMetric == null)
			{
				return NotFound();
			}
			var result = await service.Analyze(dailyMetric);
			return Ok(result);
		}

		[HttpGet("me/range")]
		public async Task<ActionResult<IEnumerable<DailyMetricReturnModel>>> GetDailyMetricsByDateRange([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
		{
			var userId = userClaimsService.ClaimId(User);
			if (endDate < startDate)
			{
				return BadRequest("End date must be after the start date.");
			}
			var dailyMetrics = await repository.GetByUserIdAndDateRangeAsync(userId, startDate, endDate);
			var result = mapper.Map<IEnumerable<DailyMetricReturnModel>>(dailyMetrics);
			return Ok(result);
		}

		[HttpGet("today")]
		public async Task<ActionResult<DailyMetricReturnModel>> GetDailyMetricForToday()
		{
			var userId = userClaimsService.ClaimId(User);
			var today = DateOnly.FromDateTime(DateTime.Today);
			Console.WriteLine(today.ToString());
			var dailyMetric = await repository.GetByUserIdAndDateAsync(userId, today);
			if (dailyMetric == null)
			{
				return NotFound();
			}
			var result = mapper.Map<DailyMetricReturnModel>(dailyMetric);
			return Ok(result);
		}

		[HttpPost("today")]
		public async Task<IActionResult> AddOrUpdateDailyMetricForToday([FromBody] AddOrUpdateModel model)
		{
			var userId = userClaimsService.ClaimId(User);
			Console.WriteLine(userId);
			var today = DateOnly.FromDateTime(DateTime.Today);
			var existingMetric = await repository.GetByUserIdAndDateAsync(userId, today);

			if (existingMetric == null)
			{
				var dailyMetric = new DailyMetric() { 
					Id = Guid.NewGuid(),
					Date = today,
					UserId = userId,
					Weight = model.Weight,
					Height = model.Height,
					SystolicBloodPressure = model.SystolicBloodPressure,
					DiastolicBloodPressure = model.DiastolicBloodPressure,
					HeartRate = model.HeartRate,
					Steps = model.Steps,
					BodyTemperature	= model.BodyTemperature,
				};
				await repository.AddAsync(dailyMetric);
				return NoContent();
			}
			else
			{
				existingMetric.Weight = model.Weight;
				existingMetric.Height = model.Height;
				existingMetric.SystolicBloodPressure = model.SystolicBloodPressure;
				existingMetric.DiastolicBloodPressure = model.DiastolicBloodPressure;
				existingMetric.HeartRate = model.HeartRate;
				existingMetric.Steps = model.Steps;
				existingMetric.BodyTemperature = model.BodyTemperature;
				await repository.UpdateAsync(existingMetric);
				return NoContent();
			}
		}
	}

	public class AddOrUpdateModel
	{
		public double Weight { get; set; }
		public double Height { get; set; }
		public int SystolicBloodPressure { get; set; }
		public int DiastolicBloodPressure { get; set; }
		public int HeartRate { get; set; }
		public int Steps { get; set; }
		public double BodyTemperature { get; set; }
	}

}
