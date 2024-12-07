using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IRepositories
{
	public interface IDoctorRepository
	{
		Task<Doctor?> GetDoctorByIdAsync(Guid doctorId);
		Task<Doctor?> GetDoctorByUserIdAsync(Guid userId);
		Task AddDoctorAsync(Doctor doctor);
		Task UpdateDoctorAsync(Doctor doctor);
		Task DeleteDoctorAsync(Guid doctorId);
		Task<IList<Doctor>> GetAllDoctors(string? nameQuery, string? cityQuery, bool? getAll);
	}
}
