using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.Employee.Request;
using Worigo.Core.Dtos.Employee.Response;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.User.Request;
using Worigo.Core.Extension;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : CustomBaseController
    {
        private readonly IEmployeesService _employeesService;
        private readonly IEmployeesTypeService _employeesTypeService;
        private readonly IUserRoleService _userRoleService;
        private readonly IUserService _userService;
        private readonly IHotelService _hotelService;
        private readonly IManagementOfHotelService _managementOfHotelService;
        private readonly IDirectorsDepartmansService _directorsDepartmansService;
        private readonly IMapper _mapper;
        public EmployeesController(IHotelService hotelService, IUserService userService,
            IManagementOfHotelService managementOfHotelService, IDirectorsDepartmansService directorsDepartmansService,
            IUserRoleService userRoleService, IEmployeesTypeService employeesTypeService,
            IEmployeesService employeesService, IMapper mapper)
        {
            _employeesService = employeesService;
            _hotelService = hotelService;
            _managementOfHotelService = managementOfHotelService;
            _directorsDepartmansService = directorsDepartmansService;
            _mapper = mapper;
            _employeesTypeService = employeesTypeService;
            _userRoleService = userRoleService;
            _userService = userService;
        }
        [HttpGet("{hotelid}")]
        public ResponseDto<List<EmployeeResponse>> GetAll([FromHeader] string Authorization, int hotelid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _employeesService.GetEmployeesByHotelId(keys, hotelid);
        }
        [HttpGet("{id}")]
        public ResponseDto<EmployeeResponse> GetById([FromHeader] string Authorization, int id)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _employeesService.GetEmployeeByEmployeeId(id, keys);
        }
        [HttpPost("{hotelid}")]
        public ResponseDto<NoContentResult> AddEmployees([FromHeader] string Authorization, [FromForm] EmployeesAndUserAddOrUpdateDto entity, int hotelid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var hotel = _hotelService.GetById(keys, hotelid);
            if ((keys.companyid == hotel.Companyid) && keys.role == 2 || keys.role == 1)
            {
                var user = new User
                {
                    companyid = hotel.Companyid,
                    email = entity.email,
                    password = entity.password,
                    roleid = 4
                };
                var userResponse = _userService.Create(_mapper.Map<UserRequest>(user),keys);
                var employee = new Employees
                {
                    StartDateOfWork = new System.DateTime(entity.ExitEntryDate.Value.Year, entity.ExitEntryDate.Value.Month, entity.ExitEntryDate.Value.Day),
                    ExitEntryDate = new System.DateTime(entity.StartDateOfWork.Value.Year, entity.StartDateOfWork.Value.Month, entity.StartDateOfWork.Value.Day),
                    Surname = entity.Surname,
                    FloorNo = entity.FloorNo,
                    gender = entity.gender,
                    hotelid = hotelid,
                    employeestypeid = entity.employeestypeid,
                    Name = entity.Name,
                    userid = user.id
                };
                var employeeResponse = _employeesService.Create(keys, _mapper.Map<EmployeeRequest>(employee));
                return new ResponseDto<NoContentResult>().Success(200);
            }
            else if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                var user = new User
                {
                    companyid = hotel.Companyid,
                    email = entity.email,
                    password = entity.password,
                    roleid = 4
                };
                var userResponse = _userService.Create(_mapper.Map<UserRequest>(user), keys);
                var employee = new Employees
                {
                    StartDateOfWork = new System.DateTime(entity.ExitEntryDate.Value.Year, entity.ExitEntryDate.Value.Month, entity.ExitEntryDate.Value.Day),
                    ExitEntryDate = new System.DateTime(entity.StartDateOfWork.Value.Year, entity.StartDateOfWork.Value.Month, entity.StartDateOfWork.Value.Day),
                    Surname = entity.Surname,
                    FloorNo = entity.FloorNo,
                    gender = entity.gender,
                    hotelid = hotelid,
                    employeestypeid = entity.employeestypeid,
                    Name = entity.Name,
                    userid = user.id
                };
                var employeeResponse = _employeesService.Create(keys, _mapper.Map<EmployeeRequest>(employee));
                return new ResponseDto<NoContentResult>().Success(200);

            }
            return new ResponseDto<NoContentResult>().Authorization();
        }
        [HttpPost("{hotelid}")]
        public ResponseDto<NoContentResult> Update([FromHeader] string Authorization, [FromForm] EmployeesAndUserAddOrUpdateDto entity, int hotelid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var hotel = _hotelService.GetById(keys, hotelid);
            string imageurl = null;
            if (entity.ImageUrl != null)
            {
                imageurl = FileToByteConvert.FromFileToByte(entity.ImageUrl);
            }
            var commentSingularData = _employeesService.GetById(entity.id,keys);
            commentSingularData.data.ImageUrl = imageurl;
            commentSingularData.data.Name = entity.Name;
            commentSingularData.data.phoneNumber = entity.phoneNumber;
            commentSingularData.data.Surname = entity.Surname;
            commentSingularData.data.FloorNo = entity.FloorNo;
            if ((keys.companyid == hotel.Companyid) && keys.role == 2 || keys.role == 1 || keys.userId == commentSingularData.data.userid)
            {
                _employeesService.Update(keys,_mapper.Map<EmployeeRequest>(commentSingularData));
                return new  ResponseDto<NoContentResult>().Success(200);
            }
            else if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                _employeesService.Update(keys, _mapper.Map<EmployeeRequest>(commentSingularData));
                return new ResponseDto<NoContentResult>().Success(200);
            }
            else if (keys.role == 5)
            {
                _directorsDepartmansService.GetDirectoryByHotelIdAndId(hotelid, keys.userId);
                _employeesService.Update(keys, _mapper.Map<EmployeeRequest>(commentSingularData));
                return new ResponseDto<NoContentResult>().Success(200);
            }
            return new ResponseDto<NoContentResult>().Authorization();
        }
        [HttpPost("{employeeId}")]
        public ResponseDto<NoContentResult> IsWorkingOrDontWork([FromHeader] string Authorization, int employeeId)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var employeedata = _employeesService.GetById(employeeId,keys);
            if (employeedata.data.OnlineOrOfflineNow == true) { employeedata.data.OnlineOrOfflineNow = false; employeedata.data.LastOnlineTime = System.DateTime.Now; }
            else
                employeedata.data.OnlineOrOfflineNow = true;
            _employeesService.Update(keys, _mapper.Map<EmployeeRequest>( employeedata));
            return new ResponseDto<NoContentResult>().Success(200);
        }
        [HttpGet("{hotelid}")]
        public IActionResult IsOnlineEmployeeList([FromHeader] string Authorization, int hotelid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var commentList = _employeesService.GetEmployeesByHotelId(keys, hotelid);
            return CreateActionResult(commentList);
        }
    }
}
