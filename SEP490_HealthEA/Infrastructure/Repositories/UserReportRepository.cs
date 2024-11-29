using Domain.Interfaces.IRepositories;
using Domain.Models.Entities;
using Infrastructure.SQLServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
	public class UserReportRepository : IUserReportRepository
	{
		private readonly SqlDBContext context;

		public UserReportRepository(SqlDBContext context)
		{
			this.context = context;
		}

		public async Task AddUserReportAsync(UserReport report)
		{
			context.UserReports.Add(report);
			await context.SaveChangesAsync();
		}

		public async Task<UserReport?> GetReportByIdAsync(Guid reportId)
		{
			return await context.UserReports
				.Include(r => r.Reporter)
				.FirstOrDefaultAsync(r => r.Id == reportId);
		}

		public async Task<IEnumerable<UserReport>> GetAllReportsAsync(int? status = null)
		{
			var query = context.UserReports.AsQueryable();

			if (status.HasValue)
			{
				query = query.Where(r => r.Status == status);
			}

			return await query.ToListAsync();
		}

		public async Task MarkReportStatusAsync(Guid reportId, int status)
		{
			var report = await context.UserReports.FindAsync(reportId);

			if (report != null)
			{
				report.Status = status;
				if (status == 1)
				{
					report.ResolvedAt = DateTime.UtcNow;
				} else
				{
					report.ResolvedAt = null;
				}
				await context.SaveChangesAsync();
			}
		}
	}
}
