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
		Task<DailyMetricAnalysisResult> Analyze(DailyMetric metric);
	}

	public class DailyMetricAnalysisResult
	{
		public DailyMetricAnalysisResult(DailyMetric metric)
		{
			Metric = metric;
		}

		public DailyMetric Metric { get; set; }
		public List<DailyMetricWarning> Warnings { get; set; } = new List<DailyMetricWarning>();
	}

	public class DailyMetricWarning
	{
		public string MetricName { get; set; }
		public string Description { get; set; }
	}
}
