using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
	public class UserReport
	{
		public Guid Id { get; set; }

		public Guid ReporterId { get; set; }
		public User Reporter { get; set; }
		public string ReportType { get; set; }
		public string ReportDescription { get; set; }
		public Guid ReportedId {  get; set; }
		public int Status { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? ResolvedAt { get; set; }
	}

}
