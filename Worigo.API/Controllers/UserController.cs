﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Worigo.API.Model;
using Worigo.API.Model.UserViewModel;
using Worigo.Business.Abstrack;
using Worigo.Business.EmailEntegre;
using Worigo.Core.Dtos.DirectorsDepartmans.Request;
using Worigo.Core.Dtos.DirectorsDepartmans.Response;
using Worigo.Core.Dtos.Employee.Request;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ManagerDto.Request;
using Worigo.Core.Dtos.ManagerDto.Response;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.User.Request;
using Worigo.Core.Dtos.User.Response;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IHotelService _hotelService;
        private readonly IEmployeesService _employeesService;
        private readonly IUserRoleService _userRoleService;
        private readonly ICompaniesService _companiesService;
        private readonly IManagementOfHotelService _managementOfHotelService;
        private readonly IResetPasswordForCodeService _resetPasswordForCodeService;
        private readonly IDirectorsDepartmansService _directorsDepartmansService;
        TokenViewModel tkn = new TokenViewModel();
        public UserController(IEmployeesService employeesService, IResetPasswordForCodeService resetPasswordForCodeService,
            IManagementOfHotelService managementOfHotelService, ICompaniesService companiesService,
            IUserRoleService userRoleService, IDirectorsDepartmansService directorsDepartmansService,
            IMapper mapper, IUserService userService, IHotelService hotelService)
        {
            _mapper = mapper;
            _userService = userService;
            _resetPasswordForCodeService = resetPasswordForCodeService;
            _hotelService = hotelService;
            _userRoleService = userRoleService;
            _employeesService = employeesService;
            _companiesService = companiesService;
            _managementOfHotelService = managementOfHotelService;
            _directorsDepartmansService = directorsDepartmansService;
        }
        [HttpPost]
        [AllowAnonymous]
        public ResponseDto<TokenViewModel> Login(LoginViewModel loginViewModel)
        {

            var user = _userService.GetUserByEmailAndPassword(loginViewModel.email, loginViewModel.password);
            if (user != null)
            {

                var token = _userService.ProduceToken(user.data.id.ToString(), user.data.email, user.data.roleid.ToString(), user.data.companyid.ToString(), loginViewModel.deviceId);
                tkn.Token = token;
                return new ResponseDto<TokenViewModel>().Success(tkn, 200);
            }
            return new ResponseDto<TokenViewModel>().Fail(400, "Your Email And Password Is Wrong");
        }
        [HttpPost]
        public ResponseDto<UserResponse> Update([FromHeader] string Authorization, UserRequest entity)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _userService.Update(entity, keys);
        }
        [HttpGet("{id}")]
        public ResponseDto<UserResponse> GetById([FromHeader] string Authorization, int id)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _userService.GetById(id, keys);

        }
        [HttpGet]
        public ResponseDto<List<UserResponse>> List([FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _userService.GetAllJoin(keys);
        }
        [HttpGet("{hotelid}")]
        public ResponseDto<List<UserResponse>> GetUserList([FromHeader] string Authorization, int hotelid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _userService.GetUserByHotelid(hotelid, keys);
        }

        [HttpPost("{email}")]
        public IActionResult FargotPassword(string email)
        {
            //var user = _userService.GetUserByEmail(email);
            //var code = RandomGeneration.RandomVertificationCodeCreate(6);
            //EmailSend.SendMail(email, "Code For Reset Password", code.ToString());
            //var entity = new ResetPasswordForCode
            //{
            //    code = code,
            //    userid = user.id
            //};
            //_resetPasswordForCodeService.Create(entity);
            return null;
        }
        [HttpGet("{code}")]
        public IActionResult ResetPassword(int code)
        {
            var cod = _resetPasswordForCodeService.GetCodeByCode(code);
            return null;
        }

        #region FOR HotelAdmin

        [HttpPost("{companiesid}")]
        public ResponseDto<NoContentResult> AddHotelAdminByCompaniesid([FromHeader] string Authorization, AddHotelAdminModelDto modelDto, int companiesid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            _companiesService.GetById(companiesid);
            if (keys.role == 1)
            {
                var user = new UserRequest
                {
                    email = modelDto.email,
                    password = modelDto.password,
                    roleid = 2,
                    companyid = companiesid,
                };
                var userResponse = _userService.Create(user, keys);

                var admin = new Employees
                {
                    Name = modelDto.name,
                    Surname = modelDto.surname,
                    ImageUrl = modelDto.imageurl,
                    phoneNumber = modelDto.phonenumber,
                    gender = modelDto.gender,
                    CreatedDate = System.DateTime.Now,
                    ModifyDate = System.DateTime.Now,
                    StartDateOfWork = new System.DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                    ExitEntryDate = new System.DateTime(0001, 01, 01),
                    isActive = true,
                    isDeleted = false,
                    FloorNo = 0,
                    hotelid = null,
                    userid = userResponse.data.id,
                    employeestypeid = 2
                };
                _employeesService.Create(keys, _mapper.Map<EmployeeRequest>(admin));
                return new ResponseDto<NoContentResult>().Success(200);
            }
            return new ResponseDto<NoContentResult>().Authorization();
        }
        [HttpGet("{companiesid}")]
        public ResponseDto<List<CampanyAndHotelJoinList>> GetHotelAdminByCompaniesid([FromHeader] string Authorization, int companiesid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return new ResponseDto<List<CampanyAndHotelJoinList>>().Success(_hotelService.GetHotelsByCompanyid(companiesid), 200);
        }
        [HttpPost]
        public ResponseDto<NoContentResult> UpdateHotelAdmin([FromHeader] string Authorization, AddHotelAdminModelDto request)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var userid = _userService.GetById(request.AdminUserId, keys);
            if (keys.role == 2 && (keys.companyid == userid.data.companyid) || keys.role == 1)
            {
                userid.data.password = request.password;
                var data = _userService.Update(_mapper.Map<UserRequest>(userid), keys);
                var employee = _employeesService.GetEmployeeByUserId(request.AdminUserId, keys);
                employee.data.Name = request.name;
                employee.data.Surname = request.surname;
                employee.data.ImageUrl = request.imageurl;
                employee.data.phoneNumber = request.phonenumber;
                _employeesService.Update(keys, _mapper.Map<EmployeeRequest>(employee));
                return new ResponseDto<NoContentResult>().Success(200);
            }
            return new ResponseDto<NoContentResult>().Authorization();
        }
        [HttpGet("{adminUserId}")]
        public ResponseDto<AddHotelAdminModelDto> GetHotelAdminById([FromHeader] string Authorization, int adminUserId)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _userService.GetHotelAdminByAdminUserId(adminUserId, keys);
        }
        #endregion

        #region FOR Managers

        [HttpPost("{hotelid}")]
        public ResponseDto<NoContentResult> AddManagementByHotelid([FromHeader] string Authorization, ManagementAddDto modelDto, int hotelid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var hotel = _hotelService.GetById(keys, hotelid);
            if ((keys.companyid == hotel.Companyid) && keys.role == 2 || keys.role == 1)
            {
                var entity = new UserRequest
                {
                    companyid = hotel.Companyid,

                    email = modelDto.email,
                    password = modelDto.password,
                    roleid = 3,

                };
                var user = _userService.Create(entity, keys);

                var employees = new Employees
                {
                    Name = modelDto.name,
                    Surname = modelDto.surname,
                    StartDateOfWork = new DateTime(modelDto.StartDateOfWork.Value.Year, modelDto.StartDateOfWork.Value.Month, modelDto.StartDateOfWork.Value.Day, modelDto.StartDateOfWork.Value.Hour
                    , modelDto.StartDateOfWork.Value.Minute, modelDto.StartDateOfWork.Value.Second),
                    ExitEntryDate = modelDto.ExitEntryDate,
                    employeestypeid = 3,
                    CreatedDate = System.DateTime.Now,
                    FloorNo = null,
                    gender = true,
                    hotelid = hotelid,
                    ImageUrl = modelDto.imageurl,
                    isActive = true,
                    isDeleted = false,
                    ModifyDate = System.DateTime.Now,
                    phoneNumber = modelDto.phonenumber,
                    userid = user.data.id
                };
                var employee = _employeesService.Create(keys, _mapper.Map<EmployeeRequest>(employees));
                var managementofhotel = new ManagementOfHotels
                {
                    hotelid = hotelid,
                    managementid = user.data.id
                };
                _managementOfHotelService.Create(managementofhotel);
                return new ResponseDto<NoContentResult>().Success(200);
            }
            return new ResponseDto<NoContentResult>().Authorization();
        }
        [HttpGet("{hotelid}")]
        public ResponseDto<List<ManagementResponse>> GetManagementByHotelId([FromHeader] string Authorization, int hotelid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var data = _userService.GetManagemetByHotelid(keys, hotelid);
            return new ResponseDto<List<ManagementResponse>>().Success(data.data, 200);
        }
        [HttpPost]
        public ResponseDto<NoContentResult> ManagementUpdate([FromHeader] string Authorization, ManagementAddDto modelDto)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _employeesService.ManagerUpdate(modelDto, keys);
        }

        [HttpGet("{managementId}")]
        public ResponseDto<ManagementResponse> GetManagementById([FromHeader] string Authorization, int managementId)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _employeesService.GetManagementById(managementId, keys);
        }

        [HttpPost("{hotelid}/{managementid}")]
        public ResponseDto<NoContentResult> RemoveManagementByHotelid([FromHeader] string Authorization, int hotelid, int managementid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var hotel = _hotelService.GetById(keys, hotelid);
            if ((keys.companyid == hotel.Companyid) && keys.role == 2 || keys.role == 1)
            {
                var managementvalue = _managementOfHotelService.GetManagementBymanagementIdByHotelid(managementid, hotelid);
                managementvalue.isDeleted = true;
                managementvalue.isActive = false;
                _managementOfHotelService.Update(managementvalue);
                return new ResponseDto<NoContentResult>().Success(200);
            }
            return new ResponseDto<NoContentResult>().Authorization();
        }

        //[HttpGet("{companiesid}")]
        //public IActionResult GetManagementByCompaniesid([FromHeader] string Authorization, int companiesid)
        //{
        //    TokenKeys keys = AuthorizationCont.Authorization(Authorization);
        //    if ((keys.companyid == companiesid) && keys.role == 2 || keys.role == 1)
        //    {
        //        var join = _userService.GetManagemetByCompaniesid(companiesid);
        //        return CreateActionResult(ResponseDto<List<ManagementResponse>>.Success(join, 200));
        //    }
        //    return CreateActionResult(ResponseDto<List<UserAndUserRoleJoin>>.Authorization());
        //}
        #endregion

        #region FOR Directories
        [HttpPost]
        public ResponseDto<NoContentResult> AddDirectorsByDepartman([FromHeader] string Authorization, UserAndDirectoryDepartmentAddOrUpdateRequest model)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var hotel = _hotelService.GetById(keys, model.hotelid);
            if (keys.role == 2 & (keys.companyid == hotel.Companyid) || keys.role == 1)
            {
                var entity = new UserRequest
                {
                    companyid = hotel.Companyid,

                    email = model.email,
                    password = model.password,
                    roleid = 5,

                };
                var user = _userService.Create(entity, keys);

                var employees = new Employees
                {
                    Name = model.name,
                    Surname = model.surname,
                    StartDateOfWork = new DateTime(model.StartDateOfWork.Year, model.StartDateOfWork.Month, model.StartDateOfWork.Day, model.StartDateOfWork.Hour
                   , model.StartDateOfWork.Minute, model.StartDateOfWork.Second),
                    ExitEntryDate = model.ExitEntryDate != null ? new DateTime(model.ExitEntryDate.Value.Year, model.ExitEntryDate.Value.Month, model.ExitEntryDate.Value.Day, model.ExitEntryDate.Value.Hour, model.ExitEntryDate.Value.Minute, model.ExitEntryDate.Value.Second) : null,
                    employeestypeid = 10,
                    CreatedDate = System.DateTime.Now,
                    FloorNo = 0,
                    gender = true,
                    hotelid = model.hotelid,
                    ImageUrl = model.imageurl,
                    isActive = true,
                    isDeleted = false,
                    ModifyDate = System.DateTime.Now,
                    phoneNumber = model.phonenumber,
                    userid = user.data.id
                };
                var employee = _employeesService.Create(keys, _mapper.Map<EmployeeRequest>(employees));
                var departmentDirectoryAdd = new DirectorsDepartmans
                {
                    departmanid = model.currentDepartmentId,
                    hotelid = model.hotelid,
                    directoryid = employee.data.EmployeeId
                };
                _directorsDepartmansService.Create(departmentDirectoryAdd);
                return new ResponseDto<NoContentResult>().Success(200);
            }
            else if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, model.hotelid);
                var entity = new UserRequest
                {
                    companyid = hotel.Companyid,

                    email = model.email,
                    password = model.password,
                    roleid = 5,

                };
                var user = _userService.Create(entity, keys);
                var employees = new Employees
                {
                    Name = model.name,
                    Surname = model.surname,
                    StartDateOfWork = new DateTime(model.StartDateOfWork.Year, model.StartDateOfWork.Month, model.StartDateOfWork.Day, model.StartDateOfWork.Hour
                   , model.StartDateOfWork.Minute, model.StartDateOfWork.Second),
                    ExitEntryDate = new DateTime(model.ExitEntryDate.Value.Year, model.ExitEntryDate.Value.Month, model.ExitEntryDate.Value.Day, model.ExitEntryDate.Value.Hour, model.ExitEntryDate.Value.Minute, model.ExitEntryDate.Value.Second),
                    employeestypeid = 10,
                    CreatedDate = System.DateTime.Now,
                    gender = true,
                    hotelid = model.hotelid,
                    ImageUrl = model.imageurl,
                    isActive = true,
                    isDeleted = false,
                    ModifyDate = System.DateTime.Now,
                    phoneNumber = model.phonenumber,
                    userid = user.data.id
                };
                var employee = _employeesService.Create(keys, _mapper.Map<EmployeeRequest>(employees));
                var departmentDirectoryAdd = new DirectorsDepartmans
                {
                    departmanid = model.currentDepartmentId,
                    hotelid = model.hotelid,
                    directoryid = employee.data.EmployeeId
                };
                _directorsDepartmansService.Create(departmentDirectoryAdd);
                return new ResponseDto<NoContentResult>().Success(200);
            }
            return new ResponseDto<NoContentResult>().Authorization();
        }
        [HttpPost]
        public IActionResult UpdateDirectoryUser([FromHeader] string Authorization, UserAndDirectoryDepartmentAddOrUpdateRequest model)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return CreateActionResult(_userService.DirectoryUpdate(keys, model));
        }
        [HttpGet("{directoryemployeeId}")]
        public IActionResult GetDirectoryByDirectoryEmployeeId([FromHeader] string Authorization, int directoryEmployeeId)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return CreateActionResult(_userService.GetDirectoryByDirectoryUserId(keys, directoryEmployeeId));
        }
        [HttpGet("{hotelid}")]
        public ResponseDto<List<UserAndDirectoryResponse>> GetDirectorsByHotelid([FromHeader] string Authorization, int hotelid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var hotel = _hotelService.GetById(keys, hotelid);
            if (keys.role == 2 && (hotel.Companyid == keys.companyid) || keys.role == 1)
            {
                var list = _employeesService.GetDirectoryByHotelid(hotelid,keys);
                return list;
            }
            if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);

                var list = _employeesService.GetDirectoryByHotelid(hotelid,keys);
                return list;
            }
            return new ResponseDto<List<UserAndDirectoryResponse>>().Authorization();
        }
        [HttpGet("{hotelid}/{departmanid}")]
        public ResponseDto<List<UserAndDirectoryResponse>> GetDirectoryByDepartmanIdAndHotelId([FromHeader] string Authorization, int hotelid, int departmanid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var directoryAndHotel = _employeesService.GetDirectoryByHotelidAndDepartmanId(keys, hotelid, departmanid);
            return directoryAndHotel;
        }
        [HttpPost]//İD İLE ÇIKARMA
        public IActionResult RemoveDepartmentDirectory([FromHeader] string Authorization, UserAndDirectoryDepartmentAddOrUpdateRequest request)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var directoryResult = _directorsDepartmansService.ToDepartmentManagerRemove(keys, request);
            return CreateActionResult(directoryResult);
        }
        #endregion

    }
}
