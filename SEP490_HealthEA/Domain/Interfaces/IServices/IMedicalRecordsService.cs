using Domain.Models;
using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IServices
{
    public interface IMedicalRecordsService
    {

        /// <summary>
        /// Get all heal profile with information on token of user
        /// </summary>
        /// <param name="claim">
        /// information of token</param>
        /// <returns>list healprofile</returns>
        /// @author: gitznos
        public ServiceResult GetAllHealProfileByToken(ClaimsPrincipal claim);
        /// <summary>
        /// Get common infomation of health profile by id
        /// </summary>
        /// <param name="claim">inforamtion token</param>
        /// <param name="id">id health profile</param>
        /// <returns>object infomation healprofile</returns>
        /// @author: gitzno
        public ServiceResult GetCommonInfoHealProfileById(ClaimsPrincipal claim, Guid id);
        /// <summary>
        /// add new health profile with token 
        /// </summary>
        /// <param name="claims">token</param>
        /// <param name="profile">info common health profile</param>
        /// <returns></returns>
        public ServiceResult AddNewHealthProfile(ClaimsPrincipal claims, HealthProfileInput profile);
        /// <summary>
        /// remove health profile for owner
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResult RemoveHealthProfile(ClaimsPrincipal claims, Guid id);
        /// <summary>
        /// update Share
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResult UpdateShareHealthProfil(ClaimsPrincipal claims, Guid id, int stone);


    }
}
