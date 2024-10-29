using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IServices
{
	public interface IUserClaimsService : IBaseServices
	{
		string ClaimAccount(ClaimsPrincipal claim);
		Guid ClaimId(ClaimsPrincipal claim);
		string ClaimRole(ClaimsPrincipal claim);
	}
}
