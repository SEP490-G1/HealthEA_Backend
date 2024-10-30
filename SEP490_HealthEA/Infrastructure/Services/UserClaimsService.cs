using Domain.Interfaces.IRepositories;
using Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class UserClaimsService : IUserClaimsService
	{
		private readonly IUserRepository repository;

		public UserClaimsService(IUserRepository repository)
		{
			this.repository = repository;
		}

		public string ClaimAccount(ClaimsPrincipal claim)
		{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
			string? _username = claim.Identity.Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
			if (_username == null)
			{
				return "";
			}
			return _username;
		}
		public string ClaimRole(ClaimsPrincipal claim)
		{
			string? role = claim.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
			if (role == null)
			{
				return "";
			}
			return role;
		}
		public Guid ClaimId(ClaimsPrincipal claim)
		{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
			var _username = ClaimAccount(claim);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

			var guid = repository.GetGuidByUserName(_username);
			return guid;
		}
	}
}
