using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
	public class Doctor
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public User? User { get; set; }

		public string DisplayName { get; set; }
		public string? Description { get; set; }
		public string ClinicAddress { get; set; }
		public string ClinicCity { get; set; }
		public string Specialization { get; set; }
		public int NumberOfAppointments { get; set; } = 0;
		public int NumberOfVideoCalls { get; set; } = 0;
		public string HistoryOfWork { get; set; } //json
	}
}
