using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.FoodMenu.Request;
using Worigo.Core.Dtos.FoodMenu.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class FoodMenuController : CustomBaseController
    {
        private readonly IFoodMenuService _foodMenuService;
        private readonly IMapper _mapper;
        private readonly IHotelService _hotelService;
        private readonly IDirectorsDepartmansService _directorsDepartmansService;
        private readonly IManagementOfHotelService _managementOfHotelService;
        public FoodMenuController(IHotelService hotelService, IFoodMenuService foodMenuService, IMapper mapper,
            IDirectorsDepartmansService directorsDepartmansService, IManagementOfHotelService managementOfHotelService)
        {
            _foodMenuService = foodMenuService;
            _mapper = mapper;
            _hotelService = hotelService;
            _directorsDepartmansService = directorsDepartmansService;
            _managementOfHotelService = managementOfHotelService;
        }

        [HttpPost]
        public ResponseDto<FoodMenuResponse> NewMenu([FromHeader] string Authorization, FoodMenuRequest request)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _foodMenuService.Create(request, keys);

        }
        [HttpGet("{hotelid}")]
        public ResponseDto<List<FoodMenuResponse>> MenuListByHotelId([FromHeader] string Authorization, int hotelid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _foodMenuService.GetMenuByHotelId(hotelid, keys);

        }
        [HttpGet("{id}")]
        public ResponseDto<FoodMenuResponse> GetMenuById([FromHeader] string Authorization, int id)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _foodMenuService.GetById(id, keys);
        }
        [HttpPost]
        public ResponseDto<FoodMenuResponse> MenuUpdate([FromHeader] string Authorization, FoodMenuRequest model)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _foodMenuService.Update(model, keys);

        }
    }
}
;
