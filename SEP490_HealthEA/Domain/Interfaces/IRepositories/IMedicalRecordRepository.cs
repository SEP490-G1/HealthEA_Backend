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
        public IList<HealthProfile> GetAllHealthProfileByUser(string userName);
        public HealthProfile? HealthProfileDetailbyID(Guid id);
        /// <summary>
        /// Add new health profile
        /// </summary>
        /// <param name="healthProfile">healprofile</param>
        /// <returns>row change</returns>
        public int AddNewHealthProfile(HealthProfile healthProfile);
        /// <summary>
        /// remove delete health profile
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int RemoveHealthProfile(Guid id);
        /// <summary>
        /// update share
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int ShareHealthProfile(Guid id, int stone);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="healthProfile"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateHealthProfile(HealthProfile healthProfile, Guid id);
        public DocumentProfile GetDocumentProfiles(int type, Guid id, Guid PantientId);
        public int CreateDocumentProfile(DocumentProfile doc);
    }
}
