using Domain.Interfaces.IRepositories;
using Domain.Interfaces.IServices;
using Domain.Models.Entities.YourNamespace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class DailyMetricAnalysisService : IDailyMetricsAnalysisService
	{
		public async Task<DailyMetricAnalysisResult> Analyze(DailyMetric metric)
		{
			var result = new DailyMetricAnalysisResult(metric);
			// Check weight (this example assumes weight in kg and height in cm)
			double bmi = metric.Weight / Math.Pow(metric.Height / 100.0, 2);
			if (bmi < 18.5)
				result.Warnings.Add(new DailyMetricWarning()
				{
					MetricName = "bmi",
					Description = "Underweight (BMI below 18.5)"
				});
			else if (bmi >= 25)
				result.Warnings.Add(new DailyMetricWarning() {
					MetricName = "bmi",
					Description = "Overweight (BMI above 25)"
				});

			// Check blood pressure
			if (metric.SystolicBloodPressure > 130)
				result.Warnings.Add(new DailyMetricWarning() {
					MetricName = "bloodPressure",
					Description = "High systolic blood pressure (above 130)"
				});
			if (metric.DiastolicBloodPressure > 80)
				result.Warnings.Add(new DailyMetricWarning()
				{
					MetricName = "bloodPressure",
					Description = "High diastolic blood pressure (above 80)"
				});

			// Check heart rate
			if (metric.HeartRate < 50)
				result.Warnings.Add(new DailyMetricWarning() {MetricName = "heartRate", Description = "Low heart rate (below 50 bpm)" });
			else if (metric.HeartRate > 100)
				result.Warnings.Add(new DailyMetricWarning() {MetricName = "heartRate", Description = "High heart rate (above 100 bpm)" });

			// Check body temperature
			if (metric.BodyTemperature < 36.0)
				result.Warnings.Add(new DailyMetricWarning() {MetricName = "bodyTemperature", Description = "Low body temperature (below 36°C)" });
			else if (metric.BodyTemperature > 37.5)
				result.Warnings.Add(new DailyMetricWarning() {MetricName = "bodyTemperature", Description = "High body temperature (above 37.5°C)" });

			// Check step count
			if (metric.Steps < 5000)
				result.Warnings.Add(new DailyMetricWarning() {MetricName = "steps", Description = "Low step count (below 5,000 steps)" });

			return await Task.FromResult(result);
		}
	}
}
