

using Domain.Models.Entities;

namespace Domain.Interfaces.IRepositories
{
	public interface IDailyMetricRepository
	{
		Task<DailyMetric?> GetByIdAsync(Guid id);
		Task<IEnumerable<DailyMetric>> GetAllByUserIdAsync(Guid userId);
		Task AddAsync(DailyMetric dailyMetric);
		Task UpdateAsync(DailyMetric dailyMetric);
		Task DeleteAsync(Guid id);
		Task<IEnumerable<DailyMetric>> GetByUserIdAndDateRangeAsync(Guid userId, DateOnly startDate, DateOnly endDate);
		Task<DailyMetric?> GetByUserIdAndDateAsync(Guid userId, DateOnly date);
		Task<T?> GetMostRecentValueAsync<T>(Guid userId, System.Linq.Expressions.Expression<Func<DailyMetric, T?>> field) where T : struct;
		Task<DailyMetric?> GetLatestByUserId(Guid userId);
	}
}
