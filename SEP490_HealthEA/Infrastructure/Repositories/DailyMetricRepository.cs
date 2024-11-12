using Domain.Interfaces.IRepositories;
using Domain.Models.Entities;
using Infrastructure.SQLServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
	public class DailyMetricRepository : IDailyMetricRepository
	{
		private readonly SqlDBContext context;

		public DailyMetricRepository(SqlDBContext context)
		{
			this.context = context;
		}

		public async Task<DailyMetric?> GetByIdAsync(Guid id)
		{
			return await context.DailyMetrics
				.Include(dm => dm.User)
				.FirstOrDefaultAsync(dm => dm.Id == id);
		}

		public async Task<IEnumerable<DailyMetric>> GetAllByUserIdAsync(Guid userId)
		{
			return await context.DailyMetrics
				.Where(dm => dm.UserId == userId)
				.ToListAsync();
		}

		public async Task AddAsync(DailyMetric dailyMetric)
		{
			await context.DailyMetrics.AddAsync(dailyMetric);
			await context.SaveChangesAsync();
		}

		public async Task UpdateAsync(DailyMetric dailyMetric)
		{
			context.DailyMetrics.Update(dailyMetric);
			await context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid id)
		{
			var dailyMetric = await GetByIdAsync(id);
			if (dailyMetric != null)
			{
				context.DailyMetrics.Remove(dailyMetric);
				await context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<DailyMetric>> GetByUserIdAndDateRangeAsync(Guid userId, DateOnly startDate, DateOnly endDate)
		{
			return await context.DailyMetrics
				.Where(dm => dm.UserId == userId && dm.Date >= startDate && dm.Date <= endDate)
				.ToListAsync();
		}

		public async Task<DailyMetric?> GetByUserIdAndDateAsync(Guid userId, DateOnly date)
		{
			return await context.DailyMetrics
				.FirstOrDefaultAsync(dm => dm.UserId == userId && dm.Date == date);
		}

		public async Task<DailyMetric?> GetLatestByUserId(Guid userId)
		{
			return await context.DailyMetrics
				.FirstOrDefaultAsync(dm => dm.UserId == userId);
		}

		public async Task<T?> GetMostRecentValueAsync<T>(Guid userId, Expression<Func<DailyMetric, T?>> field) where T : struct
		{
			return await context.DailyMetrics
				.Where(m => m.UserId == userId && field.Body != null)
				.OrderByDescending(m => m.Date)
				.Select(field)
				.FirstOrDefaultAsync();
		}
	}
}
