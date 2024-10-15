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
            var healthProfile = _context.HealthProfiles.FirstOrDefault(x => x.Id == id);
            return healthProfile;
        }

        public int RemoveHealthProfile(Guid id)
        {
            // Lấy danh sách các bản ghi liên quan cần xóa
            var relatedRecords = _context.DocumentProfiles.Where(r => r.PantientId == id);
            var real = HealthProfileDetailbyID(id);
            // Xóa các bản ghi liên quan
            _context.DocumentProfiles.RemoveRange(relatedRecords);
            _context.HealthProfiles.RemoveRange(real);
            return _context.SaveChanges();
        }

        public int ShareHealthProfile(Guid id, int stone)
        {
            var real = HealthProfileDetailbyID(id);
            real.SharedStatus = stone;
            return _context.SaveChanges();
        }
    }
}
