using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.Core.Dtos.DirectorsDepartmans.Response;
using Worigo.Core.Dtos.Employee.Request;
using Worigo.Core.Dtos.Employee.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ManagerDto.Request;
using Worigo.Core.Dtos.ManagerDto.Response;
using Worigo.Core.Dtos.ResponseDtos;

namespace Worigo.Business.Abstrack
{
    public interface IEmployeesService
    {
        ResponseDto<List<UserAndDirectoryResponse>> GetDirectoryByHotelidAndDepartmanId(TokenKeys keys, int hotelid, int departmanid);
        ResponseDto<List<UserAndDirectoryResponse>> GetDirectoryByHotelid(int hotelid, TokenKeys keys);
        ResponseDto<List<EmployeeResponse>> GetEmployeesByHotelId(TokenKeys data, int hotelid);
        ResponseDto<EmployeeResponse> GetEmployeeByEmployeeId(int employeeId, TokenKeys keys);
        ResponseDto<EmployeeResponse> Create(TokenKeys data, EmployeeRequest request);
        ResponseDto<EmployeeResponse> Update(TokenKeys data, EmployeeRequest request);
        ResponseDto<EmployeeResponse> GetById(int id,TokenKeys keys);
        ResponseDto<NoContentResult> ManagerUpdate(ManagementAddDto model,TokenKeys keys);
        ResponseDto<ManagementResponse> GetManagementById(int managerUserId, TokenKeys data);
        ResponseDto<EmployeeResponse> GetEmployeeByUserId(int userId, TokenKeys data);
    }
}
