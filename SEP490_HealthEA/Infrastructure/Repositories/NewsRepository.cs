using Domain.Interfaces.IRepositories;
using Domain.Models.Entities;
using Infrastructure.SQLServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
	public class NewsRepository : INewsRepository
	{
		private readonly SqlDBContext context;

		public NewsRepository(SqlDBContext context)
		{
			this.context = context;
		}

		public async Task<IEnumerable<News>> GetAllAsync()
		{
			return await context.News
				.OrderByDescending(n => n.CreatedAt)
				.ToListAsync();
		}

		public async Task<News?> GetByIdAsync(Guid id)
		{
			return await context.News.FindAsync(id);
		}

		public async Task<News?> GetLatestAsync()
		{
			return await context.News.OrderByDescending(n => n.CreatedAt).FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<News>> GetByCategoryAsync(string category)
		{
			return await context.News
				.Where(n => n.Category == category)
				.OrderByDescending(n => n.CreatedAt)
				.ToListAsync();
		}

		public async Task AddAsync(News news)
		{
			context.News.Add(news);
			await context.SaveChangesAsync();
		}

		public async Task UpdateAsync(News news)
		{
			context.News.Update(news);
			await context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid id)
		{
			var news = await GetByIdAsync(id);
			if (news != null)
			{
				context.News.Remove(news);
				await context.SaveChangesAsync();
			}
		}
	}
}
