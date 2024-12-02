using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        public Guid GetGuidByUserName(string username);
		Task<User?> GetUserByIdAsync(Guid userId);
	}
}
