using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DAO.Doctor
{
	public class UserReportAddDto
	{
		public string ReportType { get; set; }
		public string ReportDescription { get; set; }
		public Guid ReportedId { get; set; }
	}
}
