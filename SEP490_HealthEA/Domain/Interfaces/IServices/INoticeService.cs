using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IServices
{
	public interface INoticeService
	{
		Task CreateAndSendNoticeAsync(Notice notice, string title);
	}
}
