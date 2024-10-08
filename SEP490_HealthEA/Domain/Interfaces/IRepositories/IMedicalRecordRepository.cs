using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IRepositories
{
    public interface IMedicalRecordRepository
    {
        /// <summary>
        /// list of profile user created
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>List all profile of patient by user id</returns>
        /// @author: thuyht/gitzno
        /// @date: 06/10/2024
        public IList<PatientProfile> GetAllPatientProfilesByUser(Guid id);
        
        public IList<MedicalRecord> GetListMedicalRecordByType(int type);
    }
}
