using Domain.Interfaces.IRepositories;
using Domain.Models.Entities.YourNamespace.Models;
using Infrastructure.SQLServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
	public class DailyMetricRepository : IDailyMetricRepository
	{
		private readonly SqlDBContext _context;

		public DailyMetricRepository(SqlDBContext context)
		{
			_context = context;
		}

		public async Task<DailyMetric?> GetByIdAsync(Guid id)
		{
			return await _context.DailyMetrics
				.Include(dm => dm.User)
				.FirstOrDefaultAsync(dm => dm.Id == id);
		}

		public async Task<IEnumerable<DailyMetric>> GetAllByUserIdAsync(Guid userId)
		{
			return await _context.DailyMetrics
				.Where(dm => dm.UserId == userId)
				.ToListAsync();
		}

		public async Task AddAsync(DailyMetric dailyMetric)
		{
			await _context.DailyMetrics.AddAsync(dailyMetric);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(DailyMetric dailyMetric)
		{
			_context.DailyMetrics.Update(dailyMetric);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid id)
		{
			var dailyMetric = await GetByIdAsync(id);
			if (dailyMetric != null)
			{
				_context.DailyMetrics.Remove(dailyMetric);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<DailyMetric>> GetByUserIdAndDateRangeAsync(Guid userId, DateTime startDate, DateTime endDate)
		{
			return await _context.DailyMetrics
				.Where(dm => dm.UserId == userId && dm.Date >= startDate && dm.Date <= endDate)
				.ToListAsync();
		}
	}
}
