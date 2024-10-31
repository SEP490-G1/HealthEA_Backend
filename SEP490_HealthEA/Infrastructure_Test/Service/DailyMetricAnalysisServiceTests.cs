using Domain.Models.Entities.YourNamespace.Models;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_Test.Service
{
	public class DailyMetricAnalysisServiceTests
	{
		private readonly DailyMetricAnalysisService service;

		public DailyMetricAnalysisServiceTests()
		{
			service = new DailyMetricAnalysisService();
		}

		[Theory]
		[InlineData(50, 170, "bmi", "Underweight (BMI below 18.5)")]
		[InlineData(80, 170, "bmi", "Overweight (BMI above 25)")]
		public async Task Analyze_ShouldReturnBmiWarning(double weight, double height, string expectedMetricName, string expectedDescription)
		{
			var metric = new DailyMetric { Weight = weight, Height = height };
			var result = await service.Analyze(metric);
			Assert.Contains(result.Warnings, w => w.MetricName == expectedMetricName && w.Description == expectedDescription);
		}

		[Theory]
		[InlineData(140, 85, "bloodPressure", "High systolic blood pressure (above 130)")]
		[InlineData(120, 90, "bloodPressure", "High diastolic blood pressure (above 80)")]
		public async Task Analyze_ShouldReturnBloodPressureWarning(int systolic, int diastolic, string expectedMetricName, string expectedDescription)
		{
			var metric = new DailyMetric { SystolicBloodPressure = systolic, DiastolicBloodPressure = diastolic };
			var result = await service.Analyze(metric);
			Assert.Contains(result.Warnings, w => w.MetricName == expectedMetricName && w.Description == expectedDescription);
		}

		[Theory]
		[InlineData(45, "heartRate", "Low heart rate (below 50 bpm)")]
		[InlineData(105, "heartRate", "High heart rate (above 100 bpm)")]
		public async Task Analyze_ShouldReturnHeartRateWarning(int heartRate, string expectedMetricName, string expectedDescription)
		{
			var metric = new DailyMetric { HeartRate = heartRate };
			var result = await service.Analyze(metric);
			Assert.Contains(result.Warnings, w => w.MetricName == expectedMetricName && w.Description == expectedDescription);
		}

		[Theory]
		[InlineData(35.5, "bodyTemperature", "Low body temperature (below 36°C)")]
		[InlineData(38.0, "bodyTemperature", "High body temperature (above 37.5°C)")]
		public async Task Analyze_ShouldReturnBodyTemperatureWarning(double temperature, string expectedMetricName, string expectedDescription)
		{
			var metric = new DailyMetric { BodyTemperature = temperature };
			var result = await service.Analyze(metric);
			Assert.Contains(result.Warnings, w => w.MetricName == expectedMetricName && w.Description == expectedDescription);
		}

		[Theory]
		[InlineData(4000, "steps", "Low step count (below 5,000 steps)")]
		public async Task Analyze_ShouldReturnStepCountWarning(int steps, string expectedMetricName, string expectedDescription)
		{
			var metric = new DailyMetric { BloodSugar = steps };
			var result = await service.Analyze(metric);
			Assert.Contains(result.Warnings, w => w.MetricName == expectedMetricName && w.Description == expectedDescription);
		}
	}
}
