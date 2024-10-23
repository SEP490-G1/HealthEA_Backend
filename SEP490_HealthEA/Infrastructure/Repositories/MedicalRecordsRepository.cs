using Domain.Interfaces.IRepositories;
using Domain.Models.DAO;
using Domain.Models.Entities;
using Infrastructure.SQLServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Infrastructure.Repositories
{
    public class MedicalRecordsRepository : IMedicalRecordRepository
    {
        private SqlDBContext _context;
        private IUserRepository _userContext;
        public MedicalRecordsRepository(SqlDBContext context, IUserRepository user)
        {
            _context = context;
            _userContext = user;
        }

        public int AddNewHealthProfile(HealthProfile healthProfile)
        {
            _context.HealthProfiles.Add(healthProfile);
            return _context.SaveChanges();
        }

        public int CreateDocumentProfile(DocumentProfile doc)
        {
            _context.DocumentProfiles.Add(doc);
            ; return _context.SaveChanges();
        }

        public int DeleteDocumentProfilebyId(Guid id, Guid idUser)
        {
            DocumentProfile? doc = _context.DocumentProfiles.FirstOrDefault(doz => doz.Id == id && doz.UserId == idUser);
            if (doc == null)
            {
                return 0;
            }
            _context.DocumentProfiles.Remove(doc);
            return _context.SaveChanges();
        }

        public IList<HealthProfile> GetAllHealthProfileByUser(string username)
        {
            Guid userId = _userContext.GetGuidByUserName(username);
            if (userId == Guid.Empty)
            {
                return new List<HealthProfile>();
            }
            var list = _context.HealthProfiles.Where(x => x.UserId == userId).ToList();
            return list;
        }

        public DocumentProfile GetDocumentProfiles(int type, Guid id, Guid PantientId)
        {
            var doc = _context.DocumentProfiles.FirstOrDefault(x => x.UserId == id && x.Type == type && x.PantientId == PantientId);
            if (doc == null)
            {
                return new DocumentProfile();
            }
            return doc;
        }

        public DocumentProfile GetDocumentProfilesDetailbyId(Guid ids)
        {
            var doc = _context.DocumentProfiles.Include(dp => dp.HealthProfile).FirstOrDefault(dp => dp.Id == ids);
            if (doc == null)
            {
                return new DocumentProfile();
            }
            return doc;
        }

        public HealthProfile? HealthProfileDetailbyID(Guid id)
        {
            var healthProfile = _context.HealthProfiles.FirstOrDefault(x => x.Id == id);
            if (healthProfile == null)
            {
                return null;
            }
            return healthProfile;
        }

        public int RemoveHealthProfile(Guid id)
        {
            // Lấy danh sách các bản ghi liên quan cần xóa
            var relatedRecords = _context.DocumentProfiles.Where(r => r.PantientId == id);
            var real = HealthProfileDetailbyID(id);
            if (real == null)
            {
                return 0;
            }
            // Xóa các bản ghi liên quan
            _context.DocumentProfiles.RemoveRange(relatedRecords);
            _context.HealthProfiles.RemoveRange(real);
            return _context.SaveChanges();
        }

        public int ShareHealthProfile(Guid id, int stone)
        {
            var real = HealthProfileDetailbyID(id);
            if (real == null)
            {
                return 0;
            }
            real.SharedStatus = stone;
            return _context.SaveChanges();
        }

        public int UpdateDocumentProfile(Guid idUser, Guid id, DocumentProfileInputDAO doc)
        {
            var entity = _context.DocumentProfiles.FirstOrDefault(item => item.Id == id && item.UserId == idUser);
            if (entity == null)
            {
                return -1;
            }
            //move anther healprofile
            entity.PantientId = doc.HealthProfileId;
            entity.Type = doc.Type;
            entity.ContentMedical = doc.ContentMedical;
            entity.Image = doc.Image;
            return _context.SaveChanges();
        }

        public int UpdateHealthProfile(HealthProfile healthProfile, Guid id)
        {
            var entity = _context.HealthProfiles.FirstOrDefault(item => item.Id == id);

            if (entity != null)
            {
                // Answer for question #2

                // Make changes on entity

                entity.FullName = healthProfile.FullName;
                entity.DateOfBirth = healthProfile.DateOfBirth;
                entity.Gender = healthProfile.Gender;
                entity.Residence = healthProfile.Residence;
                entity.Note = healthProfile.Note;
                /* If the entry is being tracked, then invoking update API is not needed. 
                  The API only needs to be invoked if the entry was not tracked. 
                  https://www.learnentityframeworkcore.com/dbcontext/modifying-data */
                // context.Products.Update(entity);

                // Save changes in database
                return _context.SaveChanges();
            }
            return _context.SaveChanges();
        }
    }
}
