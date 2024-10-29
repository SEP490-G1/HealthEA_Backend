using Domain.Interfaces.IRepositories;
using Domain.Interfaces.IServices;
using Domain.Models.Entities.YourNamespace.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Customer
{
	[Route("api/[controller]")]
	[ApiController]
	public class DailyMetricController : ControllerBase
	{
		private readonly IDailyMetricRepository repository;
		private readonly IDailyMetricsAnalysisService service;

		public DailyMetricController(IDailyMetricRepository repository, IDailyMetricsAnalysisService service)
		{
			this.repository = repository;
			this.service = service;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<DailyMetric>> GetDailyMetricById(Guid id)
		{
			var dailyMetric = await repository.GetByIdAsync(id);
			if (dailyMetric == null)
			{
				return NotFound();
			}
			return Ok(dailyMetric);
		}

		[HttpGet("user/{userId}")]
		public async Task<ActionResult<IEnumerable<DailyMetric>>> GetDailyMetricsByUserId(Guid userId)
		{
			var dailyMetrics = await repository.GetAllByUserIdAsync(userId);
			return Ok(dailyMetrics);
		}

		[HttpPost]
		public async Task<ActionResult> CreateDailyMetric([FromBody] DailyMetric dailyMetric)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			dailyMetric.Id = new Guid();
			dailyMetric.Date = DateTime.Today;
			await repository.AddAsync(dailyMetric);
			return CreatedAtAction(nameof(GetDailyMetricById), new { id = dailyMetric.Id }, dailyMetric);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateDailyMetric([FromBody] DailyMetric dailyMetric)
		{
			var existingMetric = await repository.GetByIdAsync(dailyMetric.Id);
			if (existingMetric == null)
			{
				return NotFound();
			}
			dailyMetric.Date = DateTime.Today;
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

		[HttpGet("user/{userId}/range")]
		public async Task<ActionResult<IEnumerable<DailyMetric>>> GetDailyMetricsByDateRange(Guid userId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
		{
			if (endDate < startDate)
			{
				return BadRequest("End date must be after the start date.");
			}

			var dailyMetrics = await repository.GetByUserIdAndDateRangeAsync(userId, startDate, endDate);
			return Ok(dailyMetrics);
		}

		[HttpGet("user/{userId}/today")]
		public async Task<ActionResult<DailyMetric>> GetDailyMetricForToday(Guid userId)
		{
			var today = DateTime.Today;
			var dailyMetric = await repository.GetByUserIdAndDateAsync(userId, today);
			if (dailyMetric == null)
			{
				return NotFound();
			}
			return Ok(dailyMetric);
		}

		[HttpPost("user/{userId}/today")]
		public async Task<IActionResult> AddOrUpdateDailyMetricForToday(Guid userId, [FromBody] DailyMetric dailyMetric)
		{
			if (dailyMetric == null || dailyMetric.UserId != userId)
			{
				return BadRequest("Invalid daily metric data.");
			}

			var today = DateTime.Today;
			var existingMetric = await repository.GetByUserIdAndDateAsync(userId, today);

			if (existingMetric == null)
			{
				dailyMetric.Id = Guid.NewGuid();
				dailyMetric.Date = today;
				await repository.AddAsync(dailyMetric);
				return CreatedAtAction(nameof(GetDailyMetricForToday), new { userId = userId }, dailyMetric);
			}
			else
			{
				existingMetric.Weight = dailyMetric.Weight;
				existingMetric.Height = dailyMetric.Height;
				existingMetric.SystolicBloodPressure = dailyMetric.SystolicBloodPressure;
				existingMetric.DiastolicBloodPressure = dailyMetric.DiastolicBloodPressure;
				existingMetric.HeartRate = dailyMetric.HeartRate;
				existingMetric.Steps = dailyMetric.Steps;
				existingMetric.BodyTemperature = dailyMetric.BodyTemperature;

				await repository.UpdateAsync(existingMetric);
				return NoContent();
			}
		}
	}
}
