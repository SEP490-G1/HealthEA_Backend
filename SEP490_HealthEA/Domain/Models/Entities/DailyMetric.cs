using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
	using System;

	namespace YourNamespace.Models
	{
		public class DailyMetric
		{
			public Guid Id { get; set; }
			public Guid UserId { get; set; }

			// Health data fields
			public double Weight { get; set; }
			public double Height { get; set; }
			public int SystolicBloodPressure { get; set; }
			public int DiastolicBloodPressure { get; set; }
			public int HeartRate { get; set; }
			public int Steps { get; set; }
			public double BodyTemperature { get; set; }
			public DateTime Date { get; set; }

			// Navigation property
			public virtual User User { get; set; }
		}
	}

}
