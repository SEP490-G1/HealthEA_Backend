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
		private readonly HttpClient httpClient;

		public ImagesController(ICloudinaryService cloudinaryService, IImageRepository imageRepository, HttpClient httpClient)
		{
			this.cloudinaryService = cloudinaryService;
			this.imageRepository = imageRepository;
			this.httpClient = httpClient;
		}

		[HttpPost]
		public async Task<IActionResult> Upload([FromForm] ImageUploadPostModel model)
		{
			using var stream = model.File.OpenReadStream();
			var result = await cloudinaryService.Upload(stream);
			await imageRepository.AddImageAsync(new Image()
			{
				ImageUrl = result.Url.ToString(),
				PublicId = result.PublicId,
			});
			return Ok();
		}

		[HttpGet("get/{id}")]
		public async Task<IActionResult> Get(int id)
		{
			Image? image = await imageRepository.GetImageAsync(id);
			if (image == null)
			{
				return NotFound();
			}
			return Ok(image);
		}

		[HttpGet("img/{id}")]
		public async Task<IActionResult> GetImage(int id)
		{
			Image? image = await imageRepository.GetImageAsync(id);
			if (image == null)
			{
				//Probably should return some temp image instead
				return NotFound();
			}
			try
			{
				var response = await httpClient.GetAsync(image.ImageUrl);
				if (!response.IsSuccessStatusCode)
				{
					return BadRequest();
				}
				var contentType = response.Content.Headers.ContentType?.ToString();
				var imageData = await response.Content.ReadAsByteArrayAsync();
				// Return the image with the correct content type
				return File(imageData, contentType ?? "application/octet-stream");
			} catch (HttpRequestException)
			{
				return StatusCode(500);
			}
			
		}

	}

	public class ImageUploadPostModel
	{
		public IFormFile File { get; set; }
	}
}
