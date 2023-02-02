using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.ServicesValue.Request;
using Worigo.Core.Dtos.ServicesValue.Response;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ServiceValuesController : CustomBaseController
    {
        private readonly IServiceValueService _serviceValueService;
        public ServiceValuesController(IServiceValueService serviceValueService)
        {
            _serviceValueService = serviceValueService;
        }
        [HttpGet("{serviceid}")]
        public ResponseDto<List<ServicesValueResponse>> GetValuesByServiceId([FromHeader] string Authorization, int serviceid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _serviceValueService.GetValueByServiceId(serviceid, keys);
        }
        [HttpGet("{id}")]
        public ResponseDto<ServicesValueResponse> GetById([FromHeader] string Authorization, int id)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _serviceValueService.GetById(id, keys);
        }
        [HttpPost]
        public ResponseDto<ServicesValueResponse> Add([FromHeader] string Authorization, ServicesValuesAddOrUpdateRequest request)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _serviceValueService.Create(request, keys);
        }
        [HttpPost]
        public ResponseDto<ServicesValueResponse> Update([FromHeader] string Authorization, ServicesValuesAddOrUpdateRequest request)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _serviceValueService.Update(request, keys);
        }
        //[HttpPost]
        //public IActionResult AddTaskServiceValueByEmployeeType([FromHeader] string Authorization, ServiceValueOfEmployeeTypeAddOrUpdateRequest request)
        //{
        //    TokenKeys keys = AuthorizationCont.Authorization(Authorization);
        //    var hotel = _hotelService.GetById(keys, request.hotelid);
        //    var entity = _mapper.Map<ServiceValueOfEmployeeType>(request);
        //    if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
        //    {
        //        _serviceValueOfEmployeeTypeService.Create(entity);
        //        return CreateActionResult(ResponseDto<Services>.Success(200));
        //    }
        //    else if (keys.role == 3)
        //    {
        //        _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, request.hotelid);
        //        _serviceValueOfEmployeeTypeService.Create(entity);
        //        return CreateActionResult(ResponseDto<Services>.Success(200));
        //    }
        //    return CreateActionResult(ResponseDto<List<Services>>.Authorization());
        //}
    }
}
