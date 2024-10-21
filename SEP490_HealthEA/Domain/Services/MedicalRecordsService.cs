using AutoMapper;
using Domain.Enum;
using Domain.Interfaces.IRepositories;
using Domain.Interfaces.IServices;
using Domain.Models;
using Domain.Models.Common;
using Domain.Models.DAO;
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
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

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
            var _config = new MapperConfiguration(cfg => cfg.CreateMap<HealthProfile, HealthProfileOutputDAO>().ReverseMap());

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
                return "";
            }
            return _username;
        }
        private string claimRole(ClaimsPrincipal claim)
        {
            string? role = claim.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (role == null)
            {
                return "";
            }
            return role;
        }
        private Guid claimId(ClaimsPrincipal claim)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var _username = claimAccount(claim);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var guid = _userRepository.GetGuidByUserName(_username);
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
            IList<HealthProfileOutputDAO> listResult = new List<HealthProfileOutputDAO>();
            // if list null userMsg change
            if (!list.Any())
            {
                userMsg = UserMsg.GetEmpty;
            }
            else
            {
                //mapping 
                listResult = _HealprofileMapper.Map<List<HealthProfile>, List<HealthProfileOutputDAO>>((List<HealthProfile>)list);
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
            string devMsg = DevMsg.GetSuccess;
            string userMsg = UserMsg.GetSuccess;
            HealthProfile? res = null;
            HttpStatusCode hr = HttpStatusCode.OK;
            // - lấy health profile
            var healthProfile = _repository.HealthProfileDetailbyID(id);
            if (healthProfile == null)
            {
                return new ServiceResult()
                {
                    devMsg = DevMsg.GetError,
                    userMsg = UserMsg.GetErr,
                    statusCode = HttpStatusCode.BadRequest,
                    data = null
                };
            }
            // - kiểm tra người dùng có phải tác giả không
            // + lấy id người dùng
            Guid idUser = claimId(claim);
            // + kiểm tra có phải tác giả không
            if (healthProfile.UserId == idUser || healthProfile.SharedStatus == 3)
            {
                //   -  nếu có trả về health profile
                res = healthProfile;
            }
            //   -  nếu không kiểm trạng thái share của health profile
            //       - nếu trạng thái public trả về health profile (share == 3)
            else
            {
                //       - nếu trạng thái private thì báo lỗi (share == 0)
                if (healthProfile.SharedStatus == 0)
                {
                    return new ServiceResult()
                    {
                        devMsg = DevMsg.AuthorizeErr,
                        userMsg = UserMsg.AuthorizeErr,
                        statusCode = hr,
                        data = null
                    };
                }
                //lấy role User
                string role = claimRole(claim);
                //       - nếu trạng thái only doctor kiem tra có phải doctor không và trả về lỗi (share == 1)
                if (healthProfile.SharedStatus == 1 && role != RoleConstants.DOCTOR)
                {
                    throw new UnauthorizedAccessException("You do not have access!");
                }
                //       - nếu trạng thái only user trả kiểm tra (share == 2)
                if (healthProfile.SharedStatus == 2 && (role != RoleConstants.DOCTOR || role != RoleConstants.CUSTOMER))
                {
                    throw new UnauthorizedAccessException("You do not have access!");
                }
            }

            var ress = (res == null) ? null : _HealprofileMapper.Map<HealthProfile, HealthProfileOutputDAO>(res);
            return new ServiceResult()
            {
                devMsg = devMsg,
                userMsg = userMsg,
                statusCode = hr,
                data = ress
            };
        }

        private KeyValuePair<HealthProfileInputDAO, bool> validateProfile(HealthProfileInputDAO profile)
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
            return new KeyValuePair<HealthProfileInputDAO, bool>(profile, prime);
        }
        public ServiceResult AddNewHealthProfile(ClaimsPrincipal claims, HealthProfileInputDAO profile)
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
                Id = new Guid(),
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
            if (res <= 0)
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

        public ServiceResult RemoveHealthProfile(ClaimsPrincipal claims, Guid id)
        {
            string devMsg = DevMsg.DeleteSucess;
            string userMsg = UserMsg.DeleteSucess;
            HttpStatusCode hr = HttpStatusCode.OK;
            Guid idUser = claimId(claims);
            int res = 0;
            // - lấy health profile
            var healthProfile = _repository.HealthProfileDetailbyID(id);
            if (healthProfile == null)
            {
                devMsg = DevMsg.DeleteSucess;
                userMsg = UserMsg.DeleteErr;
                hr = HttpStatusCode.BadRequest;
            }
            else
            {
                // + kiểm tra có phải tác giả không
                if (healthProfile.UserId != idUser)
                {
                    throw new UnauthorizedAccessException("You do not have access!");
                }
                // xóa hết
                res = _repository.RemoveHealthProfile(id);
            }
            ServiceResult result = new ServiceResult()
            {
                devMsg = devMsg,
                userMsg = userMsg,
                statusCode = hr,
                data = res
            };

            return result;
        }

        public ServiceResult UpdateShareHealthProfile(ClaimsPrincipal claims, Guid id, int stone)
        {
            string devMsg = DevMsg.UpdateSuccess;
            string userMsg = UserMsg.UpdateSuccess;
            HttpStatusCode hr = HttpStatusCode.OK;
            Guid idUser = claimId(claims);
            int res = 0;
            // - lấy health profile
            var healthProfile = _repository.HealthProfileDetailbyID(id);
            if (healthProfile == null)
            {
                devMsg = DevMsg.DeleteSucess;
                userMsg = UserMsg.DeleteErr;
                hr = HttpStatusCode.BadRequest;
            }
            else
            {
                // + kiểm tra có phải tác giả không
                if (healthProfile.UserId != idUser)
                {
                    throw new UnauthorizedAccessException("You do not have access!");
                }
                // xóa hết
                res = _repository.ShareHealthProfile(id, stone);
            }
            ServiceResult result = new ServiceResult()
            {
                devMsg = devMsg,
                userMsg = userMsg,
                statusCode = hr,
                data = res
            };

            return result;
        }

        public ServiceResult UpdateInfoHealthProfile(ClaimsPrincipal claims, Guid id, HealthProfileInputDAO profile)
        {
            string devMsg = DevMsg.UpdateSuccess;
            string userMsg = UserMsg.UpdateSuccess;
            HttpStatusCode hr = HttpStatusCode.OK;
            Guid idUser = claimId(claims);
            int res = 0;
            // - lấy health profile
            var healthProfile = _repository.HealthProfileDetailbyID(id);
            if (healthProfile == null)
            {
                devMsg = DevMsg.DeleteSucess;
                userMsg = UserMsg.DeleteErr;
                hr = HttpStatusCode.BadRequest;
            }
            else
            {
                // + kiểm tra có phải tác giả không
                if (healthProfile.UserId != idUser)
                {
                    throw new UnauthorizedAccessException("You do not have access!");
                }
                if(profile.FullName == null)
                {
              
                    return new ServiceResult()
                    {
                        devMsg = DevMsg.AddErr,
                        userMsg = UserMsg.AddErr,
                        statusCode = HttpStatusCode.BadRequest,
                        data = "Không được để fullname trống"
                    }; ;
                }
                
                healthProfile.FullName = profile.FullName;

                healthProfile.DateOfBirth = profile.DateOfBirth;

                healthProfile.Note = profile.Note;

                healthProfile.Residence = profile.Residence;

                healthProfile.Gender = profile.Gender;

                res = _repository.UpdateHealthProfile(healthProfile, id);
            }
            if (res == 0)
            {
                devMsg = DevMsg.DeleteSucess;
                userMsg = UserMsg.DeleteErr;
                hr = HttpStatusCode.BadRequest;
            }
            ServiceResult result = new ServiceResult()
            {
                devMsg = devMsg,
                userMsg = userMsg,
                statusCode = hr,
                data = res
            };

            return result;
        }

        public ServiceResult createDocumentProfile(ClaimsPrincipal claims, DocumentProfileInputDAO profile)
        {
            string devMsg = DevMsg.UpdateSuccess;
            string userMsg = UserMsg.UpdateSuccess;
            HttpStatusCode hr = HttpStatusCode.OK;
            Guid idUser = claimId(claims);
            DocumentProfile doc = new DocumentProfile()
            {
                Id = Guid.NewGuid(),
                UserId = idUser,
                PantientId = profile.HealthProfileId,
                Type = profile.Type,
                ContentMedical = profile.ContentMedical,
                Image = profile.Image,
                CreateDate = DateTime.Now,
                LastModifyDate = DateTime.Now,
                Status = 0, 
            };
            var res = _repository.CreateDocumentProfile(doc);
            ServiceResult result = new ServiceResult()
            {
                devMsg = devMsg,
                userMsg = userMsg,
                statusCode = hr,
                data = res
            };

            return result;
        }


        #endregion
    }
}
