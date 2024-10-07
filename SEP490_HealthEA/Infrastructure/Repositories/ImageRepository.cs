using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models.Entities;
using Infrastructure.SQLServer;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class ImageRepository : IImageRepository
	{
		SqlDBContext context;
		public ImageRepository(SqlDBContext context)
		{
			this.context = context;
		}

		public async Task AddImageAsync(Image image)
		{
			await context.Images.AddAsync(image);
		}

		public async Task<Image?> GetImageAsync(int id)
		{
			return await context.Images.FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}
