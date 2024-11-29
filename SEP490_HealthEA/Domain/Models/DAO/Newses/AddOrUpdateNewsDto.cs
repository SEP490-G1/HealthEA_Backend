using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DAO.Newses
{
	public class AddOrUpdateNewsDto
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public string Content { get; set; }
		public string Category { get; set; }

		public string? ImageUrl { get; set; }
	}
}
