using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IServices
{
	public interface IImageScanService
	{
		Task<string> GetBloodTestAsync(MemoryStream stream);
		Task<string> GetPrescriptionAsync(MemoryStream stream);
		Task<string> GetUrinalystAsync(MemoryStream stream);
	}
}
