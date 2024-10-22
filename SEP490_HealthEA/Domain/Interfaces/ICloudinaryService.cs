using CloudinaryDotNet.Actions;
using Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface ICloudinaryService : IBaseServices
	{
		Task<DeletionResult> Delete(string publicId);
		Task<ImageUploadResult> Upload(Stream imageStream);
	}
}
