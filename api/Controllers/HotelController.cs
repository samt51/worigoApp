using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Entity.Concrete;
using Worigo.Core.Dtos.ListDto;
using Worigo.API.Model.UserViewModel;
using Microsoft.AspNetCore.Authorization;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ManagerDto.Request;
using Worigo.Core.Dtos.ManagerDto.Response;
using Worigo.Core.Dtos.Employee.Request;
using Worigo.Core.Dtos.Hotel.Response;
using Worigo.Core.Dtos.Hotel.Request;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class HotelController : CustomBaseController
    {
        private readonly IHotelService _hotelService;
        private readonly IUserService _userService;
        private readonly ICompaniesService _companiesService;
        private readonly IMapper _mapper;
        private readonly IManagementOfHotelService _managementOfHotelService;
        public HotelController(IManagementOfHotelService managementOfHotelService, ICompaniesService companiesService, IUserService userService,
            IHotelService hotelService, IMapper mapper)
        {
            _hotelService = hotelService;
            _mapper = mapper;
            _managementOfHotelService = managementOfHotelService;
            _userService = userService;
            _companiesService = companiesService;
        }
        [HttpGet("{companiesid}")]
        public ResponseDto<List<HotelResponse>> GetHotelsByCompaniesId([FromHeader] string Authorization, int companiesid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _hotelService.GetHotelByCompanyid(companiesid, keys);
        }
        [HttpGet]
        public ResponseDto<List<HotelDto>> GetAll([FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys.role == 3)
            {
                var hotels = _managementOfHotelService.GetManagementHotel(keys.userId);
                return new ResponseDto<List<HotelDto>>().Success(hotels, 200);

            }
            return new ResponseDto<List<HotelDto>>().Authorization();
        }
        [HttpGet("{id}")]
        public ResponseDto<HotelResponse> GetById([FromHeader] string Authorization, int id)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var hotelSingular = _hotelService.GetById(keys, id);
            return hotelSingular;

        }
        [HttpPost]
        public ResponseDto<HotelResponse> Add(HotelAddOrUpdateRequest hoteldto, [FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _hotelService.Create(keys, hoteldto);
        }
        [HttpPost]
        public ResponseDto<HotelResponse> Update(HotelAddOrUpdateRequest hotels, [FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _hotelService.Update(hotels, keys);
        }
        
    }
}
