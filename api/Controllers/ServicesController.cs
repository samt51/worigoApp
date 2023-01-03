using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.Services.Request;
using Worigo.Core.Dtos.Services.Response;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ServicesController : CustomBaseController
    {
        private readonly IServicesService _servicesService;
        public ServicesController(IServicesService servicesService)
        {
            _servicesService = servicesService;  
        }
        [HttpGet("{hotelid}")]
        public ResponseDto<List<ServicesResponse>> GetServiceByHotelid([FromHeader] string Authorization, int hotelid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _servicesService.GetServiceByHotelid(keys, hotelid);
        }
        [HttpGet("{id}")]
        public ResponseDto<ServicesResponse> GetById([FromHeader] string Authorization, int id)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _servicesService.GetById(id, keys);
        }
        [HttpPost]
        public ResponseDto<ServicesResponse> Add([FromHeader] string Authorization, ServicesAddOrUpdateRequest request)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _servicesService.Create(request, keys);

        }
        [HttpPost]
        public ResponseDto<ServicesResponse> Update([FromHeader] string Authorization, ServicesAddOrUpdateRequest request)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _servicesService.Update(request, keys);
        }
    }
}
