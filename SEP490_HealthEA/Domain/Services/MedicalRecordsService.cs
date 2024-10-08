using Domain.Interfaces.IServices;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Services
{
    public class MedicalRecordsService : IMedicalRecordsService
    {
        public ServiceResult GetAllHealthProfile(string username)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetAllUsername(ClaimsPrincipal claim)
        {
            string _username = claim.Identity.Name;
            //string _role = claim.Identity.;
            IList<string> _list = new List<string>();
            foreach (Claim item in claim.Claims)
            {
                string s = "CLAIM TYPE: " + item.Type + "; CLAIM VALUE: " + item.Value + "</br>";
                _list.Add(s);
            }
            return new ServiceResult()
            {
                devMsg = "Get username thành công",
                userMsg = "Tạo document thành công!",
                statusCode = HttpStatusCode.OK,
                data = new { _list  }
            };
        }

        public ServiceResult GetHealthProfileByCode(Guid id, string username)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetListMedicalRecordOfType(int type, string username)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetMedicalRecordDetail(Guid id, string username)
        {
            throw new NotImplementedException();
        }
    }
}
