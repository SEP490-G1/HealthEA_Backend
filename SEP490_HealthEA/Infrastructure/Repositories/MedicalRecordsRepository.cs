using Domain.Interfaces.IRepositories;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MedicalRecordsRepository : IMedicalRecordRepository
    {
        public IList<PatientProfile> GetAllPatientProfilesByUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<MedicalRecord> GetListMedicalRecordByType(int type)
        {
            throw new NotImplementedException();
        }
    }
}
