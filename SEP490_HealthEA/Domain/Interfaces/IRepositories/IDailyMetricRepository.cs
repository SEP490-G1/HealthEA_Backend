using Domain.Models.Entities.YourNamespace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IRepositories
{
	public interface IDailyMetricRepository
	{
		Task<DailyMetric?> GetByIdAsync(Guid id);
		Task<IEnumerable<DailyMetric>> GetAllByUserIdAsync(Guid userId);
		Task AddAsync(DailyMetric dailyMetric);
		Task UpdateAsync(DailyMetric dailyMetric);
		Task DeleteAsync(Guid id);
		Task<IEnumerable<DailyMetric>> GetByUserIdAndDateRangeAsync(Guid userId, DateTime startDate, DateTime endDate);
		Task<DailyMetric?> GetByUserIdAndDateAsync(Guid userId, DateTime date);
	}
}
