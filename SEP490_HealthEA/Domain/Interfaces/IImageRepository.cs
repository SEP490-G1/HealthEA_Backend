using Domain.Interfaces.IRepositories;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface IImageRepository : IBaseRepositories
	{
		Task<Image> AddImageAsync(Stream imageStream);
		Task<bool> DeleteImageAsync(int id);
		Task<Image?> GetImageAsync(int id);
	}
}
