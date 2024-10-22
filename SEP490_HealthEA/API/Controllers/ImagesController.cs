using Domain.Interfaces;
using Domain.Models.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

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
			IList<Image> imgs = new List<Image>();
			foreach (var file in model.Files)
			{
				using var stream = file.OpenReadStream();
				Image image = await imageRepository.AddImageAsync(stream);
				imgs.Add(image);
			}
			return Ok(imgs);
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
				var response = await new HttpClient().GetAsync(image.ImageUrl);
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

		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await imageRepository.DeleteImageAsync(id);
			if (result)
			{
				return NoContent();
			}
			return BadRequest(result);
		}

	}
	public class ImageUploadPostModel
	{
		public required IList<IFormFile> Files { get; set; }
	}
}
