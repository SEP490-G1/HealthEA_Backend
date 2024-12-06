using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DAO
{
	public class DocumentChatMessageDto
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public DateTime CreatedAt { get; set; }
		public string SenderType { get; set; }
		public string Message { get; set; }
	}
}
