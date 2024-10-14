using AutoMapper;
using Domain.Interfaces.IRepositories;
using Domain.Interfaces.IServices;
using Domain.Models;
using Domain.Models.Common;
using Domain.Models.Entities;
using Domain.Resources;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Services
{
    public class MedicalRecordsService : IMedicalRecordsService
    {
        #region properties
        private IMedicalRecordRepository _repository;
        private IUserRepository _userRepository;
        private Mapper _HealprofileMapper;
        #endregion
        #region Constructor
        public MedicalRecordsService(IMedicalRecordRepository medicalRecordRepository, IUserRepository userRepository)
        {
            _repository = medicalRecordRepository;
            _userRepository = userRepository;
            //auto mapper
            var _config = new MapperConfiguration(cfg => cfg.CreateMap<HealthProfile, HealthProfileOutput>().ReverseMap());
            _HealprofileMapper = new Mapper(_config);
        }
        #endregion
        #region Private Method
        /// <summary>
        /// take data in token
        /// </summary>
        /// <param name="claim">data in token</param>
        /// <returns></returns>
        /// @author: gitzno
        private string claimAccount(ClaimsPrincipal claim)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string? _username = claim.Identity.Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (_username == null)
            {
                throw new Exception("Username is null");
            }
            return _username;
        }
        private Guid claimId(ClaimsPrincipal claim)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var _username = claimAccount(claim);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var guid = _userRepository.GetIdUserByUserName(_username);
            return guid;
        }
        #endregion

        #region method
        public ServiceResult GetAllHealProfileByToken(ClaimsPrincipal claim)
        {
            var userName = claimAccount(claim);
            IList<HealthProfile> list = _repository.GetAllHealthProfileByUser(userName);
            string devMsg = DevMsg.GetSuccess;
            string userMsg = UserMsg.GetSuccess;
            IList<HealthProfileOutput> listResult = new List<HealthProfileOutput>();
            // if list null userMsg change
            if (!list.Any())
            {
                userMsg = UserMsg.GetEmpty;
            }
            else
            {
                //mapping 
                listResult = _HealprofileMapper.Map<List<HealthProfile>, List<HealthProfileOutput>>((List<HealthProfile>)list);
            }
            // create result
            ServiceResult result = new ServiceResult()
            {
                devMsg = devMsg,
                userMsg = userMsg,
                statusCode = HttpStatusCode.OK,
                data = listResult
            };

            return result;
        }

        public ServiceResult GetCommonInfoHealProfileById(ClaimsPrincipal claim, Guid id)
        {
            throw new NotImplementedException();
        }

        private KeyValuePair<HealthProfileInput, bool> validateProfile(HealthProfileInput profile)
        {
            bool prime = true;
            if (!(profile.Gender == 0 || profile.Gender == 1 || profile.Gender == 2))
            {
                profile.Gender = -1;
                prime = false;
            }
            if (profile.FullName == null)
            {
                profile.FullName = "Tên không được phép để trống";
                prime = false;
            }
            if (profile.FullName.Any(char.IsDigit))
            {
                profile.FullName = "Tên không được phép có số";
                prime = false;
            }
            return new KeyValuePair<HealthProfileInput, bool>(profile, prime);
        }
        public ServiceResult AddNewHealthProfile(ClaimsPrincipal claims, HealthProfileInput profile)
        {
            string devMsg = DevMsg.AddSuccess;
            string userMsg = UserMsg.AddSuccess;
            HttpStatusCode statusCode = HttpStatusCode.OK;
            //validate profile
            var prime = validateProfile(profile);
            if (prime.Value == false)
            {
                devMsg = DevMsg.AddErr;
                userMsg = UserMsg.AddErr;
                statusCode = HttpStatusCode.BadRequest;
                return new ServiceResult()
                {
                    devMsg = devMsg,
                    userMsg = userMsg,
                    statusCode = statusCode,
                    data = prime.Key
                };
            }
            //create new Data
#pragma warning disable CS8601 // Possible null reference assignment.
            HealthProfile p = new HealthProfile
            {
                PantientId = new Guid(),
                UserId = claimId(claims),
                ProfileCode = "abc",
                FullName = profile.FullName,
                DateOfBirth = profile.DateOfBirth,
                Gender = profile.Gender,
                Residence = profile.Residence,
                Note = profile.Note,
                SharedStatus = 0,
                CreateDate = DateTime.Now,
                LastModifyDate = DateTime.Now,

            };
#pragma warning restore CS8601 // Possible null reference assignment.
            var res = _repository.AddNewHealthProfile(p);
            // create result
            if(res <= 0)
            {
                devMsg = DevMsg.AddErr;
                userMsg = UserMsg.AddErr;
                statusCode = HttpStatusCode.NotImplemented;
                
            }
            ServiceResult result = new ServiceResult()
            {
                devMsg = devMsg,
                userMsg = userMsg,
                statusCode = statusCode,
                data = res
            };

            return result;
        }


        #endregion
    }
}
