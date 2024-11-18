using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DAO.Doctor
{
	public class DoctorUpdateDto
	{
		public string DisplayName { get; set; }
		public string? Description { get; set; }
		public string ClinicAddress { get; set; }
		public string ClinicCity { get; set; }
		public string Specialization { get; set; }
	}
}
