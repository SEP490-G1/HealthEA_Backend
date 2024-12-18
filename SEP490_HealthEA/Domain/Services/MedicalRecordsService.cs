﻿using AutoMapper;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Services
{
    public class MedicalRecordsService : IMedicalRecordsService
    {
        #region properties
        private IMedicalRecordRepository _repository;
        private IUserRepository _userRepository;
        private readonly IMapper _HealprofileMapper;
        #endregion
        #region Constructor
        public MedicalRecordsService(IMedicalRecordRepository medicalRecordRepository, IUserRepository userRepository, IMapper mapper)
        {
            _repository = medicalRecordRepository;
            _userRepository = userRepository;
            //auto mapper

            _HealprofileMapper = mapper;
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
        /**
         * GetAllHealProfileByToken function
         * 
         * @param ClaimsPrincipal claim
         * 
         * @return ServiceResult result
        */
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

        /**
         * GetCommonInfoHealProfileById function
         * 
         * @param ClaimsPrincipal claim
         * @param Guid id
         * 
         * @return ServiceResult
        */
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
                    devMsg = DevMsg.AuthorizeErr;
                    userMsg = UserMsg.AuthorizeErr;
                    hr = HttpStatusCode.Unauthorized;


                }
            
                //       - nếu trạng thái only user trả kiểm tra (share == 2)
                if (healthProfile.SharedStatus == 2 && (role != RoleConstants.DOCTOR && role != RoleConstants.CUSTOMER))
                {
                    devMsg = DevMsg.AuthorizeErr;
                    userMsg = UserMsg.AuthorizeErr;
                    hr = HttpStatusCode.Unauthorized;
                }
            }
            if (hr == HttpStatusCode.OK)
            {
                res = healthProfile;
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

        /**
         * validateProfile function
         * 
         * @param HealthProfileInputDAO profile
         * 
         * @return KeyValuePair<HealthProfileInputDAO, bool>
        */
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

        /**
         * AddNewHealthProfile function
         * 
         * @param ClaimsPrincipal claims
         * @param HealthProfileInputDAO profile
         * 
         * @return ServiceResult
        */
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
            var time = DateTime.UtcNow.AddHours(7);
#pragma warning disable CS8601 // Possible null reference assignment.
            HealthProfile p = new HealthProfile
            {
                Id = Guid.NewGuid(),
                UserId = claimId(claims),
                ProfileCode = "abc",
                FullName = profile.FullName,
                DateOfBirth = profile.DateOfBirth,
                Gender = profile.Gender,
                Residence = profile.Residence,
                Note = profile.Note,
                SharedStatus = 0,
                CreateDate = DateTime.UtcNow.AddHours(7),
                LastModifyDate = DateTime.UtcNow.AddHours(7),

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

        /**
         * RemoveHealthProfile function
         * 
         * @param ClaimsPrincipal claims
         * @param Guid id
         * 
         * @return ServiceResult
        */
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

        /**
         * UpdateShareHealthProfile function
         * 
         * @param ClaimsPrincipal claims
         * @param Guid id
         * @param int stone
         * 
         * @return ServiceResult
        */
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

        /**
         * UpdateInfoHealthProfile function
         * 
         * @param ClaimsPrincipal claims
         * @param Guid id
         * @param HealthProfileInputDAO profile
         * 
         * @return ServiceResult
        */
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
                if (profile.FullName == null)
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

        /**
         * createDocumentProfile function
         * 
         * @param ClaimsPrincipal claims
         * @param DocumentProfileInputDAO profile
         * 
         * @return ServiceResult
        */
        public ServiceResult createDocumentProfile(ClaimsPrincipal claims, DocumentProfileInputDAO profile)
        {
            ServiceResult result = new ServiceResult()
            {
                devMsg = DevMsg.AddSuccess,
                userMsg = UserMsg.AddSuccess,
                statusCode = HttpStatusCode.OK,
                data = null
            };

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
            try
            {
                var res = _repository.CreateDocumentProfile(doc);
                var itemNew = _HealprofileMapper.Map<DocumentProfileDTO>(doc);
                result.data = itemNew;
            }
            catch (Exception ex)
            {
                if (ex.Message == "0")
                {
                    result.devMsg = "Không tồn tại dữ liệu";
                    result.userMsg = "Không tồn tại dữ liệu";
                    result.statusCode = HttpStatusCode.BadRequest;
                    result.data = 0;
                }
                else if (ex.Message == "-1")
                {
                    result.devMsg = "Không có quyền truy cập";
                    result.userMsg = "Không có quyền truy cập";
                    result.statusCode = HttpStatusCode.Unauthorized;
                    result.data = 0;
                }
                else
                {
                    throw new Exception(ex.Message);
                }
            }
            return result;
        }

        /**
         * GetDocumentProfileDetail function
         * 
         * @param ClaimsPrincipal claims
         * @param Guid id
         * 
         * @return ServiceResult
        */
        public ServiceResult GetDocumentProfileDetail(ClaimsPrincipal claims, Guid id)
        {
            ServiceResult result = new ServiceResult()
            {
                devMsg = DevMsg.GetSuccess,
                userMsg = UserMsg.GetSuccess,
                statusCode = HttpStatusCode.OK,
                data = null
            };
            DocumentProfile doc = _repository.GetDocumentProfilesDetailbyId(id);
            if (doc == null || doc == new DocumentProfile())
            {
                result.userMsg = UserMsg.GetEmpty;
                return result;
            }


            // lấy id xét xem user có phải owner
            Guid idUser = claimId(claims);
            if (doc.UserId == idUser)
            {
                var psaas = _HealprofileMapper.Map<DocumentProfileDTO>(doc);
                result.data = psaas;
                return result;
            }
            if (doc.HealthProfile == null)
            {
                result.devMsg = DevMsg.GetError;
                result.userMsg = UserMsg.GetErr;
                result.statusCode = HttpStatusCode.BadGateway;
                result.data = null;
                return result;
            }
            int a = doc.HealthProfile.SharedStatus;
            string role = claimRole(claims);
            // nếu không phải thì lấy role và xét xem có phù hợp với chia sẻ không
            
            result.devMsg = checkRole(role, a) ? result.devMsg : "Bạn không có quyền truy cập vào tài liệu này!";
            result.userMsg = checkRole(role, a) ? result.userMsg : "Bạn không có quyền truy cập vào tài liệu này!";
            result.statusCode = checkRole(role, a) ? result.statusCode : HttpStatusCode.Unauthorized;
            var pss = _HealprofileMapper.Map<DocumentProfileDTO>(doc);
            if(result.statusCode == HttpStatusCode.Unauthorized)
            {
                pss = null;
            }
            result.data = pss;
            return result;

        }

        /**
         * checkRole function
         * 
         * @param string role
         * @param int statusID
         * 
         * @return bool
        */
        private bool checkRole(string role, int statusID)
        {
            if (statusID == 0)
            {
                return false;
            }
            if (role == "CUSTOMER" && statusID <= 1)
            {
                return false;
            }
            if (string.IsNullOrEmpty(role) && statusID <= 2)
            {
                return false;
            }
            if (role == "DOCTOR" && statusID < 1)
            {
                return false;
            }
            return true;
        }

        /**
         * DeleteDocumentProfileByid function
         * 
         * @param ClaimsPrincipal claims
         * @param Guid id
         * 
         * @return ServiceResult
        */
        public ServiceResult DeleteDocumentProfileByid(ClaimsPrincipal claims, Guid id)
        {
            ServiceResult result = new ServiceResult()
            {
                devMsg = DevMsg.DeleteSucess,
                userMsg = UserMsg.DeleteSucess,
                statusCode = HttpStatusCode.OK,
                data = null
            };
            Guid idUser = claimId(claims);
            if (idUser == Guid.Empty)
            {
                result.devMsg = DevMsg.AuthorizeErr;
                result.userMsg = UserMsg.AuthorizeErr;
                result.statusCode = HttpStatusCode.Unauthorized;
                return result;
            }
            int doc = _repository.DeleteDocumentProfilebyId(id, idUser);
            if (doc <= 0)
            {
                result.devMsg = DevMsg.DeleteErr;
                result.userMsg = UserMsg.DeleteErr;
                result.statusCode = HttpStatusCode.NotFound;
                return result;
            }
            result.data = doc;
            return result;
        }

        /**
         * UpdateDocumentProfile function
         * 
         * @param ClaimsPrincipal claims
         * @param Guid id
         * @param DocumentProfileInputDAO doc
         * 
         * @return ServiceResult
        */
        public ServiceResult UpdateDocumentProfile(ClaimsPrincipal claims, Guid id, DocumentProfileInputDAO doc)
        {
            ServiceResult result = new ServiceResult()
            {
                devMsg = DevMsg.UpdateSuccess,
                userMsg = UserMsg.UpdateSuccess,
                statusCode = HttpStatusCode.OK,
                data = null
            };
            Guid idUser = claimId(claims);
            if (idUser == Guid.Empty)
            {
                result.devMsg = DevMsg.AuthorizeErr;
                result.userMsg = UserMsg.AuthorizeErr;
                result.statusCode = HttpStatusCode.Unauthorized;
                return result;
            }

            int res = _repository.UpdateDocumentProfile(idUser, id, doc);
            if (res == 0)
            {
                result.devMsg = DevMsg.UpdateErr;
                result.userMsg = UserMsg.UpdateErr;
                result.statusCode = HttpStatusCode.NotFound;
                return result;
            }
            if (res == -1)
            {
                result.devMsg = "Người dùng không có quyền cập nhật!";
                result.userMsg = "Người dùng không có quyền cập nhật!";
                result.statusCode = HttpStatusCode.Unauthorized;
                return result;
            }

            result.data = res;
            return result;
        }
        public ServiceResult GetListDocumentProfile(ClaimsPrincipal claims, Guid idHealprofile, int type)
        {
            ServiceResult result = new ServiceResult()
            {
                devMsg = DevMsg.GetSuccess,
                userMsg = UserMsg.GetSuccess,
                statusCode = HttpStatusCode.OK,
                data = null
            };
            Guid idUser = claimId(claims);
            //if (idUser == Guid.Empty)
            //{
            //    result.devMsg = DevMsg.AuthorizeErr;
            //    result.userMsg = UserMsg.AuthorizeErr;
            //    result.statusCode = HttpStatusCode.Unauthorized;
            //    return result;
            //}
            try
            {
                var res = _repository.GetDocumentProfiles(type, idUser, idHealprofile);
                result.data = res;
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("-1"))
                {
                    result.devMsg = "Bạn không có quyền truy cập";
                    result.userMsg = "Bạn không có quyền truy cập";
                    result.statusCode = HttpStatusCode.Unauthorized;
                    result.data = new List<DocumentProfile>();
                }
                else if (ex.Message.Equals("0"))
                {
                    result.devMsg = "Hồ sơ không tồn tại";
                    result.userMsg = "Hồ sơ không tồn tại";
                    result.statusCode = HttpStatusCode.OK;
                    result.data = new List<DocumentProfile>();
                }
                else
                {
                    throw new Exception(ex.Message);
                }
            }

            return result;
        }


        #endregion
    }
}
