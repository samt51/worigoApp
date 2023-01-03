using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Business.Encryption;
using Worigo.Core.Dtos.DirectorsDepartmans.Response;
using Worigo.Core.Dtos.Employee.Request;
using Worigo.Core.Dtos.Employee.Response;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ManagerDto.Request;
using Worigo.Core.Dtos.ManagerDto.Response;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.User.Request;
using Worigo.Core.Exceptions;
using Worigo.Core.Extension;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class EmployeesManager : IEmployeesService
    {
        private readonly IEmployeesDal _employeesDal;
        private readonly IManagementOfHotelService _managementOfHotelsDal;
        private readonly IUserService _userDal;
        private readonly IHotelService _hotelDal;
        private readonly IUserRoleService _userRoleDal;
        private readonly IEmployeesTypeService _employeesTypeDal;
        private readonly IMapper _mapper;
        private readonly IDirectorsDepartmansService _directorsDepartmansService;

        public EmployeesManager(IUserRoleService userRoleDal, IEmployeesTypeService employeesTypeDal,
            IUserService userDal, IHotelService hotelDal, IEmployeesDal employeesDal, IDirectorsDepartmansService directorsDepartmansService,
            IManagementOfHotelService managementOfHotelsDal, IMapper mapper)
        {
            _employeesDal = employeesDal;
            _userRoleDal = userRoleDal;
            _hotelDal = hotelDal;
            _userDal = userDal;
            _managementOfHotelsDal = managementOfHotelsDal;
            _employeesTypeDal = employeesTypeDal;
            _mapper = mapper;
            _directorsDepartmansService = directorsDepartmansService;
        }
        public ResponseDto<EmployeeResponse> Create(TokenKeys data, EmployeeRequest request)
        {
            if (request.file != null)
            {
                request.ImageUrl = FileToByteConvert.FromFileToByte(request.file);
            }
            var employee = _employeesDal.Create(_mapper.Map<Employees>(request));
            var response = _mapper.Map<EmployeeResponse>(employee);
            return new ResponseDto<EmployeeResponse>().Success(response, 200);
        }

        public ResponseDto<List<EmployeeResponse>> GetEmployeesByHotelId(TokenKeys data, int hotelid)
        {
            var hotel = _hotelDal.GetById(data, hotelid);
            var employeeResponse = _employeesDal.GetEmployeesByHotelId(hotelid);
            if (data.role == 2 && (data.companyid == hotel.Companyid) || data.role == 1)
            {
                return new ResponseDto<List<EmployeeResponse>>().Success(employeeResponse, 200);
            }
            else if (data.role == 3)
            {
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(data.userId, hotelid);
                return new ResponseDto<List<EmployeeResponse>>().Success(employeeResponse, 200);
            }
            return new ResponseDto<List<EmployeeResponse>>().Authorization();


        }

        public List<UserAndDirectoryResponse> GetDirectoryByHotelid(TokenKeys keys, int hotelid)
        {
            return _employeesDal.GetDirectoryByHotelid(hotelid);
        }

        public ResponseDto<List<UserAndDirectoryResponse>> GetDirectoryByHotelidAndDepartmanId(TokenKeys keys, int hotelid, int departmanid)
        {

            var hotel = _hotelDal.GetById(keys, hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
            {
                var directoryAndHotel = _employeesDal.GetDirectoryByHotelidAndDepartmanId(hotelid, departmanid);
                return new ResponseDto<List<UserAndDirectoryResponse>>().Success(directoryAndHotel, 200);
            }
            if (keys.role == 3)
            {
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                var directoryAndHotel = _employeesDal.GetDirectoryByHotelidAndDepartmanId(hotelid, departmanid);
                return new ResponseDto<List<UserAndDirectoryResponse>>().Success(directoryAndHotel, 200);
            }

            return new ResponseDto<List<UserAndDirectoryResponse>>().Authorization();
        }
        public ResponseDto<ManagementResponse> GetManagementById(int managerUserId, TokenKeys keys)
        {
            var management = _userDal.GetById(managerUserId, keys);
            if (keys.userId == management.data.id || keys.role == 2 && (management.data.companyid == keys.companyid) || keys.role == 1)
            {
                var managementData = _employeesDal.GetManagementById(managerUserId);
                return new ResponseDto<ManagementResponse>().Success(managementData, 200);
            }
            return new ResponseDto<ManagementResponse>().Authorization();
        }

        public ResponseDto<NoContentResult> ManagerUpdate(ManagementAddDto model, TokenKeys keys)
        {
            var employee = _employeesDal.GetById(model.id);
            var user = _userDal.GetById(employee.userid, keys);
            if (keys.userId == user.data.id || keys.role == 2 && (keys.companyid == user.data.companyid) || keys.role == 1)
            {
                employee.Name = model.name;
                employee.Surname = model.surname;
                employee.phoneNumber = model.phonenumber;
                employee.ImageUrl = model.imageurl;
                employee.StartDateOfWork = model.StartDateOfWork;
                employee.ExitEntryDate = model.ExitEntryDate;
                user.data.password = CommodMethods.ConvertToEncryp(user.data.password);
                _employeesDal.Update(employee);
                var mapuser = _mapper.Map<UserRequest>(user);
                _userDal.Update(mapuser, keys);
                return new ResponseDto<NoContentResult>().Success(200);
            }
            return new ResponseDto<NoContentResult>().Authorization();
        }
        public ResponseDto<EmployeeResponse> GetEmployeeByEmployeeId(int employeeId, TokenKeys keys)
        {
            var employee = _employeesDal.GetById(employeeId);
            var user = _userDal.GetById(employee.userid, keys);
            if ((keys.companyid == user.data.companyid && keys.role == 2) || keys.role == 1 || keys.userId == employee.userid)
            {
                return new ResponseDto<EmployeeResponse>().Success(_employeesDal.GetEmployeeByEmployeeId(employeeId), 200);
            }
            else if (keys.role == 3)
            {
                return new ResponseDto<EmployeeResponse>().Success(_employeesDal.GetEmployeeByEmployeeId(employeeId), 200);
            }
            else if (keys.role == 5)
            {
                return new ResponseDto<EmployeeResponse>().Success(_employeesDal.GetEmployeeByEmployeeId(employeeId), 200);
            }
            return new ResponseDto<EmployeeResponse>().Authorization();
        }

        public ResponseDto<EmployeeResponse> GetById(int id, TokenKeys keys)
        {
            var employee = _employeesDal.GetById(id);
            var map = _mapper.Map<EmployeeResponse>(employee);
            return new ResponseDto<EmployeeResponse>().Success(map, 200);
        }


        public ResponseDto<List<UserAndDirectoryResponse>> GetDirectoryByHotelid(int hotelid, TokenKeys keys)
        {
            return new ResponseDto<List<UserAndDirectoryResponse>>().Success(_employeesDal.GetDirectoryByHotelid(hotelid), 200);
        }

        ResponseDto<EmployeeResponse> IEmployeesService.GetEmployeeByUserId(int userId, TokenKeys data)
        {
            var employeeResponse = _mapper.Map<EmployeeResponse>(_employeesDal.GetEmployeeByUserId(userId));
            return new ResponseDto<EmployeeResponse>().Success(employeeResponse, 200);
        }
        public ResponseDto<EmployeeResponse> Update(TokenKeys data, EmployeeRequest request)
        {
            var mopper = _employeesDal.Update(_mapper.Map<Employees>(request));
            return new ResponseDto<EmployeeResponse>().Success(_mapper.Map<EmployeeResponse>(mopper), 200);
        }
    }
}
