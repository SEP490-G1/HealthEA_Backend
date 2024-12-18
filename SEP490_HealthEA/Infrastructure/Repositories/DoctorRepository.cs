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
	public class DoctorRepository : IDoctorRepository
	{
		private readonly SqlDBContext context;

		public DoctorRepository(SqlDBContext context)
		{
			this.context = context;
		}

		public async Task<IList<Doctor>> GetAllDoctors(string? nameQuery, string? cityQuery, bool? getAll, string? specialization)
		{
			IQueryable<Doctor> doctors = context.Doctors.Include(d => d.User);
			if (!getAll.HasValue || !getAll.Value)
			{
				doctors = doctors.Where(d => d.User!.Status == "ACTIVE");
			}
			if (nameQuery != null)
			{
				doctors = doctors.Where(d => d.DisplayName.Contains(nameQuery));
			}
			if (cityQuery != null)
			{
				doctors = doctors.Where(d => d.ClinicCity != null && d.ClinicCity.Contains(cityQuery));
			}
			if (specialization != null && specialization.Length > 0)
			{
				doctors = doctors.Where(d => d.Specialization != null && d.Specialization == specialization);
			}
			return await doctors.ToListAsync();
		}

		public async Task<Doctor?> GetDoctorByIdAsync(Guid doctorId)
		{
			return await context.Doctors.Include(d => d.User)
				.FirstOrDefaultAsync(d => d.Id == doctorId);
		}

		public async Task<Doctor?> GetDoctorByUserIdAsync(Guid userId)
		{
			return await context.Doctors.Include(d => d.User)
				.FirstOrDefaultAsync(d => d.UserId == userId);
		}

		public async Task AddDoctorAsync(Doctor doctor)
		{
			context.Doctors.Add(doctor);
			await context.SaveChangesAsync();
		}

		public async Task UpdateDoctorAsync(Doctor doctor)
		{
			context.Doctors.Update(doctor);
			await context.SaveChangesAsync();
		}

		public async Task DeleteDoctorAsync(Guid doctorId)
		{
			var doctor = await GetDoctorByIdAsync(doctorId);
			if (doctor != null)
			{
				context.Doctors.Remove(doctor);
				await context.SaveChangesAsync();
			}
		}

		public async Task<List<string>?> GetDaysWithSchedulesOfDoctorAsync(Guid doctorId)
		{
			var doctor = await GetDoctorByIdAsync(doctorId);
			if (doctor == null)
			{
				return null;
			}
			return await context.Schedules
				.Where(s => s.Date >= DateTime.Now && s.DoctorId == doctorId && s.Status == "Available")
				.Select(s => s.Date.ToString("yyyy-MM-dd"))
				.Distinct()
				.ToListAsync();
		}

		public async Task<List<string?>> GetListOfSpecialization()
		{
			return await context.Doctors
				.Where(s => s.Specialization != null)
				.Select(s => s.Specialization)
				.OrderBy(s => s)
				.Distinct()
				.ToListAsync();
		}
	}
}
