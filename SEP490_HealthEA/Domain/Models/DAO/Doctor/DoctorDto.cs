using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DAO.Doctor
{
	public class DoctorDto
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public string DisplayName { get; set; }
		public string? Description { get; set; }
		public string ClinicAddress { get; set; }
		public string ClinicCity { get; set; }
	}
}
