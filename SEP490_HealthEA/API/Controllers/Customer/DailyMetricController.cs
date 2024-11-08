using AutoMapper;
using Domain.Interfaces.IRepositories;
using Domain.Interfaces.IServices;
using Domain.Models.DAO.DailyMetrics;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
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
			var userId = userClaimsService.ClaimId(User);
			if (dailyMetric.UserId != userId)
			{
				return BadRequest("You do not have permission to access this record.");
			}
			var result = mapper.Map<DailyMetricReturnModel>(dailyMetric);
			return Ok(result);
		}

		[HttpGet("me")]
		public async Task<ActionResult<IEnumerable<DailyMetricReturnModel>>> GetDailyMetricsOfUser()
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

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateDailyMetric(Guid id, [FromBody] AddOrUpdateModel model)
		{
			var existingMetric = await repository.GetByIdAsync(id);
			if (existingMetric == null)
			{
				return BadRequest("Daily Metric Does Not Exists");
			}
			var userId = userClaimsService.ClaimId(User);
			if (existingMetric.UserId != userId)
			{
				return BadRequest("You do not have permission to access this record.");
			}
			existingMetric.Date = DateOnly.FromDateTime(DateTime.Today);
			if (model.Weight.HasValue) existingMetric.Weight = model.Weight.Value;
			if (model.Height.HasValue) existingMetric.Height = model.Height.Value;
			if (model.SystolicBloodPressure.HasValue) existingMetric.SystolicBloodPressure = model.SystolicBloodPressure.Value;
			if (model.DiastolicBloodPressure.HasValue) existingMetric.DiastolicBloodPressure = model.DiastolicBloodPressure.Value;
			if (model.HeartRate.HasValue) existingMetric.HeartRate = model.HeartRate.Value;
			if (model.BloodSugar.HasValue) existingMetric.BloodSugar = model.BloodSugar.Value;
			if (model.BodyTemperature.HasValue) existingMetric.BodyTemperature = model.BodyTemperature.Value;
			if (model.OxygenSaturation.HasValue) existingMetric.OxygenSaturation = model.OxygenSaturation.Value;
			if (!existingMetric.Validate(out var error))
			{
				return BadRequest(error);
			}
			await repository.UpdateAsync(existingMetric);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteDailyMetric(Guid id)
		{
			var dailyMetric = await repository.GetByIdAsync(id);
			if (dailyMetric == null)
			{
				return BadRequest("Daily Metric Does Not Exists");
			}
			var userId = userClaimsService.ClaimId(User);
			if (dailyMetric.UserId != userId)
			{
				return BadRequest("You do not have permission to access this record.");
			}
			await repository.DeleteAsync(id);
			return NoContent();
		}

		[HttpGet("analyze/{id}")]
		[Obsolete]
		public async Task<IActionResult> AnalyzeDailyMetric(Guid id)
		{
			var dailyMetric = await repository.GetByIdAsync(id);
			if (dailyMetric == null)
			{
				return BadRequest("Daily Metric Does Not Exists");
			}
			var userId = userClaimsService.ClaimId(User);
			if (dailyMetric.UserId != userId)
			{
				return BadRequest("You do not have permission to access this record.");
			}
			var mapped = mapper.Map<DailyMetricReturnModel>(dailyMetric);
			var result = await service.Analyze(mapped);
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
				return Ok(new
				{
					Msg = "No Record"
				});
			}
			var result = mapper.Map<DailyMetricReturnModel>(dailyMetric);
			return Ok(result);
		}

		[HttpGet("detailed/today")]
		public async Task<ActionResult<DailyMetricStatusResult>> GetDetailedDailyMetricForToday()
		{
			var userId = userClaimsService.ClaimId(User);
			var today = DateOnly.FromDateTime(DateTime.Today);
			Console.WriteLine(today.ToString());
			var dailyMetric = await repository.GetByUserIdAndDateAsync(userId, today);
			if (dailyMetric == null)
			{
				return Ok(new {
					Msg = "No Record"
				});
			}
			var mapped = mapper.Map<DailyMetricReturnModel>(dailyMetric);
			var result = await service.GetStatus(mapped);
			return Ok(result);
		}

		[HttpGet("detailed/{id}")]
		public async Task<ActionResult<DailyMetricStatusResult>> GetDetailedDailyMetric(Guid id)
		{
			var dailyMetric = await repository.GetByIdAsync(id);
			if (dailyMetric == null)
			{
				return NotFound();
			}
			var userId = userClaimsService.ClaimId(User);
			if (dailyMetric.UserId != userId)
			{
				return BadRequest("You do not have permission to access this record.");
			}
			var mapped = mapper.Map<DailyMetricReturnModel>(dailyMetric);
			var result = await service.GetStatus(mapped);
			return Ok(result);
		}

		[HttpPatch("today")]
		public async Task<IActionResult> AddOrUpdateDailyMetricForToday([FromBody] AddOrUpdateModel model)
		{
			var userId = userClaimsService.ClaimId(User);
			Console.WriteLine(userId);
			var today = DateOnly.FromDateTime(DateTime.Today);
			var existingMetric = await repository.GetByUserIdAndDateAsync(userId, today);

			if (existingMetric == null)
			{
				var dailyMetric = new DailyMetric
				{
					Id = Guid.NewGuid(),
					Date = today,
					UserId = userId,
					Weight = model.Weight,
					Height = model.Height,
					SystolicBloodPressure = model.SystolicBloodPressure,
					DiastolicBloodPressure = model.DiastolicBloodPressure,
					HeartRate = model.HeartRate,
					BloodSugar = model.BloodSugar,
					BodyTemperature = model.BodyTemperature,
				};
				if (!dailyMetric.Validate(out var error))
				{
					return BadRequest(error);
				}
				await repository.AddAsync(dailyMetric);
				return NoContent();
			}
			else	{ 

				if (model.Weight.HasValue) existingMetric.Weight = model.Weight.Value;
				if (model.Height.HasValue) existingMetric.Height = model.Height.Value;
				if (model.SystolicBloodPressure.HasValue) existingMetric.SystolicBloodPressure = model.SystolicBloodPressure.Value;
				if (model.DiastolicBloodPressure.HasValue) existingMetric.DiastolicBloodPressure = model.DiastolicBloodPressure.Value;
				if (model.HeartRate.HasValue) existingMetric.HeartRate = model.HeartRate.Value;
				if (model.BloodSugar.HasValue) existingMetric.BloodSugar = model.BloodSugar.Value;
				if (model.BodyTemperature.HasValue) existingMetric.BodyTemperature = model.BodyTemperature.Value;
				if (model.OxygenSaturation.HasValue) existingMetric.OxygenSaturation = model.OxygenSaturation.Value;
				if (!existingMetric.Validate(out var error))
				{
					return BadRequest(error);
				}
				await repository.UpdateAsync(existingMetric);
				return NoContent();
			}
		}

	}

	public class AddOrUpdateModel
	{
		public double? Weight { get; set; }
		public double? Height { get; set; }
		public int? SystolicBloodPressure { get; set; }
		public int? DiastolicBloodPressure { get; set; }
		public int? HeartRate { get; set; }
		public double? BloodSugar { get; set; }
		public double? BodyTemperature { get; set; }
		public double? OxygenSaturation { get; set; }
	}

}
