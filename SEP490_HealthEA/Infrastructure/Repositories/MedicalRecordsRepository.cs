using Domain.Interfaces.IRepositories;
using Domain.Models.Entities;
using Infrastructure.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MedicalRecordsRepository : IMedicalRecordRepository
    {
        private SqlDBContext _context;

        public MedicalRecordsRepository(SqlDBContext context)
        {
            _context = context;
        }

        public int AddNewHealthProfile(HealthProfile healthProfile)
        {
            _context.HealthProfiles.Add(healthProfile);
            return _context.SaveChanges();
        }

        public IList<HealthProfile> GetAllHealthProfileByUser(string username)
        {
            Guid userId = GetGuidByUserName(username);
            var list = _context.HealthProfiles.Where(x => x.UserId == userId).ToList();
            return list;
        }

        public Guid GetGuidByUserName(string userName)
        {
            var user = _context.User.FirstOrDefault(x => x.UserName == userName);
            if (user == null)
            {
                return Guid.Empty;
            }
            return user.UserId;
        }

        public HealthProfile HealthProfileDetailbyID(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
