using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
		public class DailyMetric
		{
			public Guid Id { get; set; }
			public Guid UserId { get; set; }

			// Health data fields
			public double? Weight { get; set; }
			public double? Height { get; set; }
			public int? SystolicBloodPressure { get; set; }
			public int? DiastolicBloodPressure { get; set; }
			public int? HeartRate { get; set; }
			public double? BloodSugar { get; set; }
			public double? BodyTemperature { get; set; }
			public double? OxygenSaturation { get; set; }
			public DateOnly Date { get; set; }

			// Navigation property
			public virtual User User { get; set; }

		public bool Validate(out string validationError)
		{
			validationError = string.Empty;

			if (Weight.HasValue && (Weight.Value <= 0))
			{
				validationError = "Invalid Weight";
				return false;
			}

			if (Height.HasValue && (Height.Value <= 0))
			{
				validationError = "Invalid Height";
				return false;
			}

			if (SystolicBloodPressure.HasValue && (SystolicBloodPressure.Value <= 0))
			{
				validationError = "Invalid Systolic Blood Pressure";
				return false;
			}

			if (DiastolicBloodPressure.HasValue && (DiastolicBloodPressure.Value <= 0))
			{
				validationError = "Invalid Diastolic Blood Pressure";
				return false;
			}

			if (HeartRate.HasValue && (HeartRate.Value < 40))
			{
				validationError = "Invalid Heart Rate";
				return false;
			}

			if (BloodSugar.HasValue && (BloodSugar.Value < 0 || BloodSugar.Value > 500))
			{
				validationError = "Invalid Blood Sugar Value";
				return false;
			}

			if (BodyTemperature.HasValue && (BodyTemperature.Value < 35.0 || BodyTemperature.Value > 44.0))
			{
				validationError = "Invalid Body Temperature";
				return false;
			}

			if (OxygenSaturation.HasValue && (OxygenSaturation.Value < 0 || OxygenSaturation.Value > 100))
			{
				validationError = "Invalid Oxygen Saturation";
				return false;
			}

			return true;
		}
	}
}
