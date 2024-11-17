using AutoMapper;
using Domain.Interfaces.IRepositories;
using Domain.Models.DAO.Newses;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admin
{
	[Route("api/[controller]")]
	[ApiController]
	public class NewsController : ControllerBase
	{
		private readonly INewsRepository repository;
		private readonly IMapper mapper;

		public NewsController(INewsRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<News>>> GetAll()
		{
			var news = await repository.GetAllAsync();
			return Ok(news);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<News>> GetById(Guid id)
		{
			var news = await repository.GetByIdAsync(id);
			if (news == null)
				return NotFound();

			return Ok(news);
		}

		[HttpPost]
		public async Task<IActionResult> Create(AddOrUpdateNewsDto news)
		{
			var entity = mapper.Map<News>(news);
			await repository.AddAsync(entity);
			return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, AddOrUpdateNewsDto news)
		{
			var currentEntity = await repository.GetByIdAsync(id);
			if (currentEntity == null)
			{
				return BadRequest();
			}
			currentEntity.Title = news.Title;
			currentEntity.Author = news.Author;
			currentEntity.Category = news.Category;
			currentEntity.Content = news.Content;
			currentEntity.UpdatedAt = DateTime.Now;
			await repository.UpdateAsync(currentEntity);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var news = await repository.GetByIdAsync(id);
			if (news == null)
			{
				return BadRequest();
			}
			await repository.DeleteAsync(id);
			return NoContent();
		}
	}
}
