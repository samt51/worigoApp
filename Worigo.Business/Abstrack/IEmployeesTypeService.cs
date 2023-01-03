using System.Collections.Generic;
using Worigo.Core.Dtos.EmployeeType.Request;
using Worigo.Core.Dtos.EmployeeType.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;

namespace Worigo.Business.Abstrack
{
    public interface IEmployeesTypeService
    {
        ResponseDto<List<EmployeeTypeResponse>> GetEmployeesTypeByDepartmanid(int departmanid, TokenKeys keys);
        ResponseDto<EmployeeTypeResponse> GetById(int id, TokenKeys keys);
        ResponseDto<EmployeeTypeResponse> Create(EmployeeTypeAddOrUpdateRequest entity, TokenKeys keys);
        ResponseDto<EmployeeTypeResponse> Update(EmployeeTypeAddOrUpdateRequest entity, TokenKeys keys);
    }
}
