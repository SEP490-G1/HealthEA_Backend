using Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface IOcrService : IBaseServices
	{
		void ImageToObject<T>(Stream stream, T obj) where T : class;
	}
}
