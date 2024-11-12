using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IRepositories
{
	public interface IUserReportRepository
	{
		Task AddUserReportAsync(UserReport report);
		Task<IEnumerable<UserReport>> GetAllReportsAsync(int? status = null);
		Task<UserReport?> GetReportByIdAsync(Guid reportId);
		Task MarkReportStatusAsync(Guid reportId, int status);
	}
}
