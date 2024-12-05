using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface IOpenAIChatService
	{
		Task<string> GetResponseAsync(List<string> instructions);
	}
}
