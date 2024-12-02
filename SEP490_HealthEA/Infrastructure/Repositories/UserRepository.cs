using Domain.Interfaces.IRepositories;
using Domain.Models.Entities;
using Infrastructure.SQLServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private SqlDBContext _context;

        public UserRepository(SqlDBContext context)
        {
            _context = context;
        }
        public Guid GetGuidByUserName(string username)
        {
            var userId = _context.Users.FirstOrDefault(x => x.Username == username);
            if (userId == null)
            {
                return Guid.Empty;
            }
            return userId.UserId;
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }
    }
}
