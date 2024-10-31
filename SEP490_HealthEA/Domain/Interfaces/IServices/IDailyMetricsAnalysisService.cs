using Domain.Models.DAO.DailyMetrics;
using Domain.Models.Entities.YourNamespace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IServices
{
	public interface IDailyMetricsAnalysisService : IBaseServices
	{
		[Obsolete]
		Task<DailyMetricAnalysisResult> Analyze(DailyMetricReturnModel metric);
		Task<DailyMetricStatusResult> GetStatus(DailyMetricReturnModel metric);
	}

	public class DailyMetricAnalysisResult
	{
		public DailyMetricAnalysisResult(DailyMetricReturnModel metric)
		{
			Metric = metric;
		}

		public DailyMetricReturnModel Metric { get; set; }
		public List<DailyMetricWarning> Warnings { get; set; } = new List<DailyMetricWarning>();
	}

	public class DailyMetricWarning
	{
		public string MetricName { get; set; } = null!;
		public string Description { get; set; } = null!;
	}

	public class DailyMetricValue
	{
		public string MetricName { get; set; } = null!;
		public string? Value { get; set; }
		public int Status { get; set; }
		public string? Description { get; set; }
	}

	public class DailyMetricStatusResult
	{
		public DailyMetricStatusResult(DailyMetricReturnModel dailyMetric)
		{
			DailyMetric = dailyMetric;
		}

		public DailyMetricReturnModel DailyMetric { get; set; }
		public IList<DailyMetricValue> Values { get; set; } = new List<DailyMetricValue>();
	}
}
