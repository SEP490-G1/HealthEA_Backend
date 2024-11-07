using Domain.Models.DAO.DailyMetrics;
using Infrastructure.Services;

namespace Infrastructure_Test.Service
{
    public class DailyMetricAnalysisServiceTests
	{
		private readonly DailyMetricAnalysisService service;

		public DailyMetricAnalysisServiceTests()
		{
			service = new DailyMetricAnalysisService();
		}

	}
}
