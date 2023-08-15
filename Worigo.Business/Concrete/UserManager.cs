using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Worigo.Business.Abstrack;
using Worigo.Business.EmailEntegre;
using Worigo.Business.Encryption;
using Worigo.Core;
using Worigo.Core.Dtos.DirectorsDepartmans.Request;
using Worigo.Core.Dtos.DirectorsDepartmans.Response;
using Worigo.Core.Dtos.Employee.Request;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ManagerDto.Request;
using Worigo.Core.Dtos.ManagerDto.Response;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.User.Dto;
using Worigo.Core.Dtos.User.Request;
using Worigo.Core.Dtos.User.Response;
using Worigo.Core.Exceptions;
using Worigo.Core.Extension;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IConfiguration _configuration;
        private readonly IHotelService _hotelDal;
        private readonly IManagementOfHotelService _managementOfHotelsDal;
        private readonly IEmployeesDal _employeesDal;
        private readonly INotificationService _notificationService;
        private readonly IDirectorsDepartmansService _directorsDepartmansService;
        private readonly IMapper _mapper;

        public UserManager(IEmployeesDal employeesDal, IManagementOfHotelService managementOfHotelsDal,
            IUserDal userDal, IHotelService hotelDal, IConfiguration configuration, IDirectorsDepartmansService directorsDepartmansService, IMapper mapper, INotificationService notificationService)
        {
            _managementOfHotelsDal = managementOfHotelsDal;
            _userDal = userDal;
            _notificationService = notificationService;
            _hotelDal = hotelDal;
            _employeesDal = employeesDal;
            _mapper = mapper;
            _configuration = configuration;
            _directorsDepartmansService = directorsDepartmansService;
        }
        public ResponseDto<UserResponse> Create(UserRequest entity, TokenKeys keys)
        {
            entity.password = CommodMethods.ConvertToEncryp(entity.password);
            var map = _mapper.Map<User>(entity);
            var response = _mapper.Map<UserResponse>(_userDal.Create(map));
            return new ResponseDto<UserResponse>().Success(response, 200);
        }
        public ResponseDto<UserResponse> Update(UserRequest entity, TokenKeys keys)
        {
            var data = GetById(entity.id, keys);

            data.data.password = CommodMethods.ConvertToEncryp(entity.password);
            if (keys.userId == entity.id)
            {
                var response = _userDal.Update(_mapper.Map<User>(data));
                return new ResponseDto<UserResponse>().Success(_mapper.Map<UserResponse>(response), 200);
            }
            else if ((keys.companyid == entity.companyid) && keys.role == 2 || keys.role == 1)
            {
                var response = _userDal.Update(_mapper.Map<User>(data));
                return new ResponseDto<UserResponse>().Success(_mapper.Map<UserResponse>(response), 200);
            }
            else if (entity.roleid == 4 && keys.role == 3)
            {
                var employees = _employeesDal.GetEmployeeByUserId(entity.id);
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(keys.userId, (int)employees.hotelid);
                var response = _userDal.Update(_mapper.Map<User>(data));
                return new ResponseDto<UserResponse>().Success(_mapper.Map<UserResponse>(response), 200);
            }
            return new ResponseDto<UserResponse>().Authorization();
        }

        public ResponseDto<List<UserResponse>> GetAllJoin(TokenKeys keys)
        {
            if (keys.role == 1)
            {
                return new ResponseDto<List<UserResponse>>().Success(_userDal.GetAllJoin(), 200);
            }
            return new ResponseDto<List<UserResponse>>().Authorization();
        }

        public ResponseDto<UserResponse> GetById(int id, TokenKeys keys)
        {
            var userdata = _userDal.GetById(id);

            if (keys.userId == userdata.id)
            {
                var data = _userDal.UserGetByIdJoin(id);
                return new ResponseDto<UserResponse>().Success(data, 200);
            }
            else if ((keys.companyid == userdata.companyid) && keys.role == 2 || keys.role == 3)
            {
                var data = _userDal.UserGetByIdJoin(id);
                return new ResponseDto<UserResponse>().Success(data, 200);
            }
            else if (keys.role == 3 && userdata.roleid == 4)
            {
                var employees = _employeesDal.GetEmployeeByUserId(userdata.id);
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(keys.userId, (int)employees.hotelid);
                var data = _userDal.UserGetByIdJoin(id);
                return new ResponseDto<UserResponse>().Success(data, 200);
            }
            return new ResponseDto<UserResponse>().Authorization();
        }



        #region FOR Manager

     

        public ResponseDto<List<ManagementResponse>> GetManagemetByCompaniesid(int companiesid)
        {
            return new ResponseDto<List<ManagementResponse>>().Success(_userDal.GetManagemetByCompaniesid(companiesid), 200);
        }

        public ResponseDto<List<ManagementUserResponse>> GetManagemetByHotelid(TokenKeys token, int hotelid)
        {
            var hotel = _hotelDal.GetById(token, hotelid);
            if (token.role == 1)
                return new ResponseDto<List<ManagementUserResponse>>().Success(_userDal.GetManagemetByHotelid(hotelid), 200);
            else if ((hotel.data.Companyid == token.companyid) && token.role == 2)
            {
                return new ResponseDto<List<ManagementUserResponse>>().Success(_userDal.GetManagemetByHotelid(hotelid), 200);
            }
            else if ((token.role == 3))
            {
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(token.userId, hotelid);
                return new ResponseDto<List<ManagementUserResponse>>().Success(_userDal.GetManagemetByHotelid(hotelid), 200);
            }
            return new ResponseDto<List<ManagementUserResponse>>().Authorization();
        }
        #endregion

        public ResponseDto<UserResponse> GetUserByEmail(string email, TokenKeys keys)
        {
            return new ResponseDto<UserResponse>().Success(_mapper.Map<UserResponse>(_userDal.GetUserByEmail(email)), 200);
        }

        public ResponseDto<UserResponse> GetUserByEmailAndPassword(string email, string password)
        {
            password = CommodMethods.ConvertToEncryp(password);
            return new ResponseDto<UserResponse>().Success(_mapper.Map<UserResponse>(_userDal.GetUserByEmailAndPassword(email, password)), 200);
        }

        public ResponseDto<List<UserResponse>> GetUserByHotelid(int hotelid, TokenKeys keys)
        {
            if (keys.role == 3)
            {
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                var usergetall = _userDal.GetUserByHotelid(hotelid);
                return new ResponseDto<List<UserResponse>>().Success(usergetall, 200);
            }
            return new ResponseDto<List<UserResponse>>().Authorization();
        }



        public string ProduceToken(string id, string email, string role, string companyid, string deviceid,string lng)
        {
            if (!string.IsNullOrEmpty(deviceid))
            {
                var model = new NotificationModel
                {
                    //eWD5F7S2Rvq687c86ff_H5:APA91bHQM4ZemIU5blgZ4GCha8LnvasVnS8PG1m5ITZyuBQJL8ouXD49XPpFa-2nwHpJxWX9fgLdHt5ppJLhRjtFcD7H-rzp1vh3EQ09xKgsbaGlqG_--O7oDHE5t-Uezbhv8-cDBUJp
                    IsAndroiodDevice = true,
                    DeviceId = deviceid,
                    Title = "Login Notification",
                    Body = "Hoş Geldiniz"
                };
                var not = _notificationService.SendNotification(model);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("TokenKey"));
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userId", id),
                    new Claim(ClaimTypes.Email, email),
                    new Claim("role", role),
                    new Claim("companyid",companyid),
                    new Claim("lng",lng)

                }),
                Expires = DateTime.UtcNow.AddYears(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);
            return token;
        }

        #region FOR Directory
        public ResponseDto<NoContentResult> DirectoryUpdate(TokenKeys keys, UserAndDirectoryDepartmentAddOrUpdateRequest request)
        {
            var employee = _employeesDal.GetById(request.directoryEmployeeId);
            var user = _userDal.GetById(employee.userid);
            if (keys.userId == employee.userid || keys.role == 2 && (keys.companyid == user.companyid) || keys.role == 1)
            {
                if (request.file != null)
                {
                    employee.ImageUrl = FileToByteConvert.FromFileToByte(request.file);
                }
                employee.Name = request.Name;
                employee.Surname = request.Surname;
                employee.phoneNumber = request.phoneNumber;
                employee.StartDateOfWork = request.StartDateOfWork;
                employee.ExitEntryDate = request.ExitEntryDate;
                employee.gender = request.gender;
                user.email = request.email;
                user.password = CommodMethods.ConvertToEncryp(request.password);
                _employeesDal.Update(employee);
                _userDal.Update(user);
                return new ResponseDto<NoContentResult>().Success(200);
            }
            return new ResponseDto<NoContentResult>().Authorization();
        }

        public ResponseDto<UserAndDirectoryResponse> GetDirectoryByDirectoryUserId(TokenKeys keys, int directoryEmployeeId)
        {
            var emplooye = _employeesDal.GetById(directoryEmployeeId);
            var user = _userDal.GetById(emplooye.userid);
            if (keys.userId == user.id || keys.role == 2 && (keys.companyid == user.companyid) || keys.role == 1)
            {
                return new ResponseDto<UserAndDirectoryResponse>().Success(_employeesDal.GetDirectoryByDirectoryUserId(directoryEmployeeId), 200);
            }
            else if (keys.role == 3)
            {
                _managementOfHotelsDal.IsthereManagementByDepartmentDirectory(keys.userId, directoryEmployeeId);
                return new ResponseDto<UserAndDirectoryResponse>().Success(_employeesDal.GetDirectoryByDirectoryUserId(directoryEmployeeId), 200);
            }
            return new ResponseDto<UserAndDirectoryResponse>().Authorization();
        }

     

        

        public ResponseDto<List<ManagementResponse>> GetManagemetByCompaniesid(int companiesid, TokenKeys keys)
        {
            throw new NotImplementedException();
        }

        ResponseDto<List<ManagementUserResponse>> IUserService.GetHotelAdminByCompaniesid(int companiesid)
        {
            return new ResponseDto<List<ManagementUserResponse>>().Success(_userDal.GetHotelAdminByCompaniesId(companiesid), 200);
        }

        ResponseDto<ManagementUserResponse> IUserService.GetHotelAdminByAdminUserId(int userid, TokenKeys keys)
        {
            if (keys.role == 2 && (keys.userId == userid) || keys.role == 1)
            {
                var data = _userDal.GetHotelAdminByAdminUserId(userid);
                return new ResponseDto<ManagementUserResponse>().Success(data, 200);
            }
            return new ResponseDto<ManagementUserResponse>().Authorization();
        }









        #endregion
    }
}
