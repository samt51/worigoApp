using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.HotelOfServiceDto.Request;
using Worigo.Core.Dtos.HotelOfServiceDto.Response;
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
        private readonly IHotelOfServicesService _hotelOfServicesService;
        public ServicesController(IServicesService servicesService, IHotelOfServicesService hotelOfServicesService)
        {
            _servicesService = servicesService;
            _hotelOfServicesService = hotelOfServicesService;
        }
        [HttpGet("{hotelid}")]
        public ResponseDto<List<HotelOfServiceResponse>> GetServiceByHotelid([FromHeader] string Authorization, int hotelid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _hotelOfServicesService.GetServiceByHotelId(hotelid,keys);
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
        [HttpGet]
        public ResponseDto<List<ServicesResponse>> GetAll([FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _servicesService.GetAllService(keys);
        }
        [HttpPost]
        public ResponseDto<HotelOfServiceResponse> PostServiceByHotelId([FromHeader] string Authorization, HotelOfServiceAddOrUpdate request)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _hotelOfServicesService.PostServiceByHotelId(request, keys);
        }
        [HttpPost]
        public ResponseDto<HotelOfServiceResponse> SelectService([FromHeader] string Authorization, HotelOfServiceAddOrUpdate request)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _servicesService.SelectService(keys, request);
        }
        [HttpPost("{id}")]
        public ResponseDto<HotelOfServiceResponse> RemoveServiceById([FromHeader] string Authorization,int id)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _servicesService.RemoveServiceById(keys, id);
        }

    }
}
