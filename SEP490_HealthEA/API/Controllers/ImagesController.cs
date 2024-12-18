﻿using Domain.Interfaces;
using Domain.Models.Common;
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
		private readonly IOcrService ocrService;
		private readonly IImageRepository imageRepository;

		public ImagesController(IOcrService ocrService, IImageRepository imageRepository, HttpClient httpClient)
		{
			this.ocrService = ocrService;
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

		[HttpPost("scan")]
		public async Task<IActionResult> Scan([FromForm] ImageOcrPostModel model)
		{
			using var stream = model.File.OpenReadStream();
			var result = new OcrDailyMetricsResult();
			ocrService.ImageToObject(stream, result);
			return Ok(result);
		}

	}

	public class ImageUploadPostModel
	{
		public required IList<IFormFile> Files { get; set; }
	}

	public class ImageOcrPostModel
	{
		public required IFormFile File { get; set; }
	}
}
