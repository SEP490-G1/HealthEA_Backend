using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Interfaces;
using dotenv.net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class CloudinaryService : ICloudinaryService
	{
		private readonly Cloudinary cloudinary;
		public CloudinaryService()
		{
			DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
			cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
			cloudinary.Api.Secure = true;
		}

		public async Task<string> Upload(Stream imageStream)
		{
			var imgParams = new ImageUploadParams()
			{
				File = new FileDescription("upload", imageStream),
				UseFilename = false,
			};
			var result = await cloudinary.UploadAsync(imgParams);
			return result.Url.ToString();
		}
	}	
}
