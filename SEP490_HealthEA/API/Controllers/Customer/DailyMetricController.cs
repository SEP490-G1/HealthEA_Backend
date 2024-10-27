using Domain.Interfaces.IRepositories;
using Domain.Models.Entities.YourNamespace.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Customer
{
	[Route("api/[controller]")]
	[ApiController]
	public class DailyMetricController : ControllerBase
	{
		private readonly IDailyMetricRepository repository;

		public DailyMetricController(IDailyMetricRepository dailyMetricRepository)
		{
			repository = dailyMetricRepository;
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

			await repository.AddAsync(dailyMetric);
			return CreatedAtAction(nameof(GetDailyMetricById), new { id = dailyMetric.Id }, dailyMetric);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateDailyMetric(Guid id, [FromBody] DailyMetric dailyMetric)
		{
			if (id != dailyMetric.Id)
			{
				return BadRequest("ID mismatch");
			}

			var existingMetric = await repository.GetByIdAsync(id);
			if (existingMetric == null)
			{
				return NotFound();
			}

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
	}
}
