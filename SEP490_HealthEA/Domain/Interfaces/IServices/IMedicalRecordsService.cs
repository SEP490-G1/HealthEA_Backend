using Domain.Models;
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
        /// Get all health profile by username
        /// </summary>
        /// <param name="username">username of user</param>
        /// <returns>
        /// statuscode | usermsg     | devmsg      | data  
        /// </returns>
        /// @date: 07/10/2024
        /// @author: thuyht/gitzno
        public ServiceResult GetAllHealthProfile(string username);
        /// <summary>
        /// tesst
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public ServiceResult GetAllUsername(ClaimsPrincipal claim);
        

        public ServiceResult GetHealthProfileByCode(Guid id, string username);

        public ServiceResult GetListMedicalRecordOfType(int type, string username);
      
        public ServiceResult GetMedicalRecordDetail(Guid id, string username);
    }
}
