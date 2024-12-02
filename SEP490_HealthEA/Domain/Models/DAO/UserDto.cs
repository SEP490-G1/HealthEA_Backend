using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DAO
{
	public class UserDto
	{
		public Guid UserId { get; set; }
		public DateOnly? Dob { get; set; }
		public string Email { get; set; } = null!;
		public string? FirstName { get; set; }
		public bool? Gender { get; set; }
		public string? LastName { get; set; }
		public string? Phone { get; set; }
		public string? Role { get; set; }
		public string? Status { get; set; }
		public string Username { get; set; } = null!;
		public string? Avatar { get; set; } = null!;
	}
}
