using Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Common
{
	public class OcrDailyMetricsResult
	{
		[KeyAlias(new[] {"can"})]
		public double Weight { get; set; }
		[KeyAlias(new[] { "chieu" })]
		public double Height { get; set; }
		[KeyAlias(new[] { "blood", "huyet" })]
		public double BloodPressure { get; set; }
	}
}
