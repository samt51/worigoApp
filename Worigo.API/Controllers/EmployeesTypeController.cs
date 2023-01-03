using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.API.Model.UserViewModel;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.EmployeeType.Request;
using Worigo.Core.Dtos.EmployeeType.Response;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class EmployeesTypeController : CustomBaseController
    {
        private readonly IEmployeesTypeService _employeesTypeService;
        public EmployeesTypeController(  IEmployeesTypeService employeesTypeService )
        {
            _employeesTypeService = employeesTypeService;
        }
        [HttpGet("{id}")]
        public ResponseDto<EmployeeTypeResponse> GetById([FromHeader] string Authorization, int id)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _employeesTypeService.GetById(id, keys);
        }
        [HttpPost]
        public ResponseDto<EmployeeTypeResponse> Add([FromHeader] string Authorization, EmployeeTypeAddOrUpdateRequest request)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _employeesTypeService.Create(request, keys);

        }
        [HttpPost]
        public ResponseDto<EmployeeTypeResponse> Update([FromHeader] string Authorization, EmployeeTypeAddOrUpdateRequest request)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _employeesTypeService.Update(request, keys);

        }
        [HttpGet("{departmanid}")]
        public ResponseDto<List<EmployeeTypeResponse>> GetEmployeesTypeByDepartmanid(int departmanid, [FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _employeesTypeService.GetEmployeesTypeByDepartmanid(departmanid, keys);
        }
    }
}
