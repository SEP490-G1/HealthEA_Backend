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
			public float Weight { get; set; }
			public float Height { get; set; }
			public int SystolicBloodPressure { get; set; }
			public int DiastolicBloodPressure { get; set; }
			public int HeartRate { get; set; }
			public int Steps { get; set; }
			public float BodyTemperature { get; set; }
			public DateTime Date { get; set; }

			// Navigation property
			public User User { get; set; }
		}
	}

}
