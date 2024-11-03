using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
	public class DoctorReport
	{
		public Guid Id { get; set; }

		public Guid ReporterId { get; set; }
		public User Reporter { get; set; }

		public Guid ReportedDoctorId { get; set; }
		public Doctor ReportedDoctor { get; set; }

		public string ReportDescription { get; set; }
		public int Status { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? ResolvedAt { get; set; }
	}

}
