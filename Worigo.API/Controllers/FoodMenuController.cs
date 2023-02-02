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
        public IActionResult NewMenu([FromHeader] string Authorization, FoodMenuRequest model)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var hotel = _hotelService.GetById(keys, model.hotelid);
            var mapper = _mapper.Map<FoodMenu>(model);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
            {
                _foodMenuService.Create(mapper);
                return CreateActionResult(ResponseDto<NoContentResult>.Success(200));
            }
            else if (keys.role == 3 || keys.role == 5)
            {
                if (keys.role == 5)
                {
                    _directorsDepartmansService.GetDirectoryByHotelIdAndId(model.hotelid, keys.userId);
                    _foodMenuService.Create(mapper);
                    return CreateActionResult(ResponseDto<NoContentResult>.Success(200));
                }
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, model.hotelid);
                _foodMenuService.Create(mapper);
                return CreateActionResult(ResponseDto<NoContentResult>.Success(200));
            }
            return CreateActionResult(ResponseDto<NoContentResult>.Authorization());
        }
        [HttpGet("{hotelid}")]
        public IActionResult MenuListByHotelId([FromHeader] string Authorization, int hotelid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var hotel = _hotelService.GetById(keys, hotelid);
            var responselist = _foodMenuService.GetMenuByHotelId(hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
            {
                return CreateActionResult(ResponseDto<List<FoodMenuResponse>>.Success(responselist, 200));
            }
            else if (keys.role == 3 || keys.role == 5)
            {
                if (keys.role == 5)
                {
                    _directorsDepartmansService.GetDirectoryByHotelIdAndId(hotelid, keys.userId);
                    return CreateActionResult(ResponseDto<List<FoodMenuResponse>>.Success(responselist, 200));

                }
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                return CreateActionResult(ResponseDto<List<FoodMenuResponse>>.Success(responselist, 200));
            }
            return CreateActionResult(ResponseDto<NoContentResult>.Authorization());
        }
        [HttpGet("{id}")]
        public IActionResult GetMenuById([FromHeader] string Authorization, int id)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var menuData = _foodMenuService.GetById(id);
            var hotel = _hotelService.GetById(keys, menuData.hotelid);
            var menuDto = _mapper.Map<FoodMenuResponse>(menuData);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
            {             
                return CreateActionResult(ResponseDto<FoodMenuResponse>.Success(menuDto, 200));
            }
            else if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, menuData.hotelid);
                return CreateActionResult(ResponseDto<FoodMenuResponse>.Success(menuDto, 200));
            }
            else if (keys.role == 5)
            {
                _directorsDepartmansService.GetDirectoryByHotelIdAndId(menuData.hotelid, keys.userId);
                return CreateActionResult(ResponseDto<FoodMenuResponse>.Success(menuDto, 200));
            }
            else
                return CreateActionResult(ResponseDto<NoContentResult>.Authorization());
        }
        [HttpPost]
        public IActionResult MenuUpdate([FromHeader] string Authorization, FoodMenuRequest model)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var hotel = _hotelService.GetById(keys, model.hotelid);
            var mapdto = _mapper.Map<FoodMenu>(model);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
            {
                _foodMenuService.Update(mapdto);
                return CreateActionResult(ResponseDto<FoodMenuResponse>.Success( 200));
            }
            else if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, model.hotelid);
                _foodMenuService.Update(mapdto);
                return CreateActionResult(ResponseDto<FoodMenuResponse>.Success( 200));
            }
            else if (keys.role == 5)
            {
                _directorsDepartmansService.GetDirectoryByHotelIdAndId(model.hotelid, keys.userId);
                _foodMenuService.Update(mapdto);
                return CreateActionResult(ResponseDto<FoodMenuResponse>.Success( 200));
            }
            else
                return CreateActionResult(ResponseDto<NoContentResult>.Authorization());

        }
    }
}
;
