using Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ScanController : ControllerBase
	{
		private readonly IImageScanService imageScanService;

		public ScanController(IImageScanService imageScanService)
		{
			this.imageScanService = imageScanService;
		}

		[HttpPost("prescription")]
		public async Task<IActionResult> ImageToPrescription([FromForm] ImageScanPostModel model)
		{
			var stream = new MemoryStream();
			model.File.CopyTo(stream);
			var result = await imageScanService.GetPrescriptionAsync(stream);
			return Ok(result);
		}

		[HttpPost("bloodtest")]
		public async Task<IActionResult> ImageToBloodTest([FromForm] ImageScanPostModel model)
		{
			var stream = new MemoryStream();
			model.File.CopyTo(stream);
			var result = await imageScanService.GetBloodTestAsync(stream);
			return Ok(result);
		}

		[HttpPost("urinalyst")]
		public async Task<IActionResult> ImageToUrinalyst([FromForm] ImageScanPostModel model)
		{
			var stream = new MemoryStream();
			model.File.CopyTo(stream);
			var result = await imageScanService.GetUrinalystAsync(stream);
			return Ok(result);
		}

	}

	public class ImageScanPostModel
	{
		public IFormFile File { get; set; }
	}
}
