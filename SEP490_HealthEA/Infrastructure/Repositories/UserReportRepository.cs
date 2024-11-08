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


	}
}
