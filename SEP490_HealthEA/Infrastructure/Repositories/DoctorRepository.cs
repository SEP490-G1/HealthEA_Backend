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
		private readonly SqlDBContext _context;

		public DoctorRepository(SqlDBContext context)
		{
			_context = context;
		}

		public async Task<IList<Doctor>> GetAllDoctors(string? query)
		{
			if (query == null)
			{
				return await _context.Doctors.ToListAsync();
			}
			return await _context.Doctors.Where(d => d.DisplayName.Contains(query)).ToListAsync();
		}

		public async Task<Doctor?> GetDoctorByIdAsync(Guid doctorId)
		{
			return await _context.Doctors.Include(d => d.User)
				.FirstOrDefaultAsync(d => d.Id == doctorId);
		}

		public async Task<Doctor?> GetDoctorByUserIdAsync(Guid userId)
		{
			return await _context.Doctors.Include(d => d.User)
				.FirstOrDefaultAsync(d => d.UserId == userId);
		}

		public async Task AddDoctorAsync(Doctor doctor)
		{
			_context.Doctors.Add(doctor);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateDoctorAsync(Doctor doctor)
		{
			_context.Doctors.Update(doctor);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteDoctorAsync(Guid doctorId)
		{
			var doctor = await GetDoctorByIdAsync(doctorId);
			if (doctor != null)
			{
				_context.Doctors.Remove(doctor);
				await _context.SaveChangesAsync();
			}
		}
	}
}
