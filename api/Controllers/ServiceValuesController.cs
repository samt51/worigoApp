using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.ServiceOfValueDto.Request;
using Worigo.Core.Dtos.ServiceOfValueDto.Response;
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
        private readonly IServiceOfValueService _serviceOfValueService;
        public ServiceValuesController(IServiceValueService serviceValueService, IServiceOfValueService serviceOfValueService)
        {
            _serviceValueService = serviceValueService;
            _serviceOfValueService = serviceOfValueService;
        }
        [HttpGet("{serviceid}/{hotelId}")]
        public ResponseDto<List<ServicesValueResponse>> GetValuesByServiceId([FromHeader] string Authorization, int serviceId, int hotelId)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _serviceValueService.GetValueByServiceId(hotelId, serviceId, keys);
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
        [HttpPost]
        public ResponseDto<ServiceOfValueResponse> PostServiceValueByHotelId([FromHeader] string Authorization, ServiceOfValueAddOrUpdate request)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _serviceOfValueService.PostServiceValueByHotelId(request, keys);
        }

    }
}
