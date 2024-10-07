using Domain.Interfaces;
using Domain.Models.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImagesController : ControllerBase
	{
		private readonly ICloudinaryService cloudinaryService;
		private readonly IImageRepository imageRepository;

		public ImagesController(ICloudinaryService cloudinaryService, IImageRepository imageRepository)
		{
			this.cloudinaryService = cloudinaryService;
			this.imageRepository = imageRepository;
		}

		[HttpPost]
		public async Task<IActionResult> Upload([FromForm] ImageUploadPostModel model)
		{
			using var stream = model.File.OpenReadStream();
			var url = await cloudinaryService.Upload(stream);
			await imageRepository.AddImageAsync(new Image()
			{
				ImageUrl = url,
			});
			return Ok();
		}


	}

	public class ImageUploadPostModel
	{
		public IFormFile File { get; set; }
	}
}
