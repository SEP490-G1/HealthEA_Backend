using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
	public class DocumentChatMessage
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public DateTime CreatedAt { get; set; }
		public string SenderType { get; set; }
		public string Message { get; set; }

		public virtual User User { get; set; }
	}
}
