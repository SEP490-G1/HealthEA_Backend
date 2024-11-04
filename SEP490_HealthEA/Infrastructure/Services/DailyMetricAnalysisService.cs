using Domain.Interfaces.IServices;
using Domain.Models.DAO.DailyMetrics;

namespace Infrastructure.Services
{
	public class DailyMetricAnalysisService : IDailyMetricsAnalysisService
	{
		[Obsolete]
		public async Task<DailyMetricAnalysisResult> Analyze(DailyMetricReturnModel metric)
		{
			var result = new DailyMetricAnalysisResult(metric);

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
			if (metric.BloodSugar < 5000)
				result.Warnings.Add(new DailyMetricWarning() {MetricName = "steps", Description = "Low step count (below 5,000 steps)" });

			return await Task.FromResult(result);
		}

		public async Task<DailyMetricStatusResult> GetStatus(DailyMetricReturnModel metric)
		{
			var statusResult = new DailyMetricStatusResult(metric);

			// Calculate BMI and status if Weight and Height are not null
			if (metric.Weight.HasValue && metric.Height.HasValue)
			{
				double bmi = metric.Weight.Value / Math.Pow(metric.Height.Value / 100, 2);
				int bmiStatus = bmi < 18.5 ? 1 : bmi < 24.9 ? 2 : 3;
				string? bmiDescription = bmiStatus switch
				{
					1 => "BMI thấp (gầy)",
					2 => "BMI bình thường",
					3 => "BMI cao (thừa cân)",
					_ => null
				};
				statusResult.Values.Add(new DailyMetricValue
				{
					MetricName = "BMI",
					Value = $"{bmi:F1} kg/m²",
					Status = bmiStatus,
					Description = bmiDescription
				});
			}
			else
			{
				statusResult.Values.Add(new DailyMetricValue
				{
					MetricName = "BMI",
					Value = "0 kg/m²",
					Status = -1,
					Description = null
				});
			}

			// Blood Pressure grouping and status if values are not null
			if (metric.SystolicBloodPressure.HasValue && metric.DiastolicBloodPressure.HasValue)
			{
				int bloodPressureStatus = (metric.SystolicBloodPressure.Value, metric.DiastolicBloodPressure.Value) switch
				{
					( < 90, < 60) => 1,
					( < 120, < 80) => 2,
					_ => 3
				};
				string? bpDescription = bloodPressureStatus switch
				{
					1 => "Huyết áp thấp",
					2 => "Huyết áp bình thường",
					3 => "Huyết áp cao",
					_ => null
				};
				statusResult.Values.Add(new DailyMetricValue
				{
					MetricName = "Huyết áp",
					Value = $"{metric.SystolicBloodPressure}/{metric.DiastolicBloodPressure} mmHg",
					Status = bloodPressureStatus,
					Description = bpDescription
				});
			}
			else
			{
				statusResult.Values.Add(new DailyMetricValue
				{
					MetricName = "Huyết áp",
					Value = "0 mmHg",
					Status = -1,
					Description = null
				});
			}

			// Heart Rate status if not null
			if (metric.HeartRate.HasValue)
			{
				int heartRateStatus = metric.HeartRate.Value < 60 ? 1 : metric.HeartRate.Value <= 100 ? 2 : 3;
				string? heartRateDescription = heartRateStatus switch
				{
					1 => "Nhịp tim chậm",
					2 => "Nhịp tim bình thường",
					3 => "Nhịp tim nhanh",
					_ => null
				};
				statusResult.Values.Add(new DailyMetricValue
				{
					MetricName = "Nhịp tim",
					Value = $"{metric.HeartRate} bpm",
					Status = heartRateStatus,
					Description = heartRateDescription
				});
			}
			else
			{
				statusResult.Values.Add(new DailyMetricValue
				{
					MetricName = "Nhịp tim",
					Value = "0 bpm",
					Status = -1,
					Description = null
				});
			}

			// Blood Sugar status if not null
			if (metric.BloodSugar.HasValue)
			{
				int bloodSugarStatus = metric.BloodSugar.Value < 70 ? 1 : metric.BloodSugar.Value <= 140 ? 2 : 3;
				string? bloodSugarDescription = bloodSugarStatus switch
				{
					1 => "Đường huyết thấp",
					2 => "Đường huyết bình thường",
					3 => "Đường huyết cao",
					_ => null
				};
				statusResult.Values.Add(new DailyMetricValue
				{
					MetricName = "Đường huyết",
					Value = $"{metric.BloodSugar} mg/dL",
					Status = bloodSugarStatus,
					Description = bloodSugarDescription
				});
			}
			else
			{
				statusResult.Values.Add(new DailyMetricValue
				{
					MetricName = "Đường huyết",
					Value = "0 mg/dL",
					Status = -1,
					Description = null
				});
			}

			// Body Temperature status if not null
			if (metric.BodyTemperature.HasValue)
			{
				int temperatureStatus = metric.BodyTemperature.Value < 36.5 ? 1 : metric.BodyTemperature.Value <= 37.5 ? 2 : 3;
				string? temperatureDescription = temperatureStatus switch
				{
					1 => "Nhiệt độ thấp",
					2 => "Nhiệt độ bình thường",
					3 => "Sốt",
					_ => null
				};
				statusResult.Values.Add(new DailyMetricValue
				{
					MetricName = "Nhiệt độ",
					Value = $"{metric.BodyTemperature} °C",
					Status = temperatureStatus,
					Description = temperatureDescription
				});
			}
			else
			{
				statusResult.Values.Add(new DailyMetricValue
				{
					MetricName = "Nhiệt độ",
					Value = "0 °C",
					Status = -1,
					Description = null
				});
			}
			if (metric.OxygenSaturation.HasValue)
			{
				int oxygenStatus = metric.OxygenSaturation.Value < 95 ? 1 : metric.OxygenSaturation.Value <= 100 ? 2 : 3;
				string? oxygenDescription = oxygenStatus switch
				{
					1 => "Độ bão hòa Oxy thấp",
					2 => "Độ bão hòa Oxy bình thường",
					3 => "Độ bão hòa Oxy cao",
					_ => null
				};
				statusResult.Values.Add(new DailyMetricValue
				{
					MetricName = "Độ bão hòa Oxy",
					Value = $"{metric.OxygenSaturation} SpO2",
					Status = oxygenStatus,
					Description = oxygenDescription
				});
			}
			else
			{
				statusResult.Values.Add(new DailyMetricValue
				{
					MetricName = "Oxygen Saturation",
					Value = "0 SpO2",
					Status = -1,
					Description = null
				});
			}

			return await Task.FromResult(statusResult);
		}


	}
}
