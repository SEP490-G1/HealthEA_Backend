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
		private readonly SqlDBContext context;
		private readonly ICloudinaryService cloudinaryService;
		public ImageRepository(SqlDBContext context, ICloudinaryService cloudinaryService)
		{
			this.context = context;
			this.cloudinaryService = cloudinaryService;
		}

		public async Task<Image> AddImageAsync(Stream imageStream)
		{
			//Upload
			var result = await cloudinaryService.Upload(imageStream);
			var img = new Image()
			{
				ImageUrl = result.Url.ToString(),
				PublicId = result.PublicId,
			};
			await context.Images.AddAsync(img);
			await context.SaveChangesAsync();
			return img;
		}

		public async Task<Image?> GetImageAsync(int id)
		{
			return await context.Images.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<bool> DeleteImageAsync(int id)
		{
			Image? image = await GetImageAsync(id);
			if (image == null)
			{
				return false;
			}
			//Try deleting from Cloudinary
			var deletionResult = await cloudinaryService.Delete(image.PublicId);
			var status = (int)deletionResult.StatusCode >= 200 && (int)deletionResult.StatusCode <= 299;
			if (status)
			{
				context.Remove(image);
				await context.SaveChangesAsync();
				return true;
			}
			return false;
		}
	}
}
