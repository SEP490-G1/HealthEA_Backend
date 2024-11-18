using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IRepositories
{
	public interface INewsRepository
	{
		Task<IEnumerable<News>> GetAllAsync();
		Task<News?> GetByIdAsync(Guid id);
		Task<IEnumerable<News>> GetByCategoryAsync(string category);
		Task AddAsync(News news);
		Task UpdateAsync(News news);
		Task DeleteAsync(Guid id);
		Task<News?> GetLatestAsync();
	}
}
