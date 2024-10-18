using Domain.Interfaces.IRepositories;
using Infrastructure.SQLServer;
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
        public Guid GetIdUserByUserName(string username)
        {
            var userId = _context.User.FirstOrDefault(x => x.Username == username);
            if (userId == null)
            {
                return Guid.Empty;
            }
            return userId.UserId;
        }
    }
}
