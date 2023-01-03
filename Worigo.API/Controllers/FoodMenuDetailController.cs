using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.FoodMenuDetailDto.Dto;
using Worigo.Core.Dtos.FoodMenuDetailDto.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class FoodMenuDetailController : CustomBaseController
    {
        private readonly IFoodMenuDetailService _foodMenuDetailService;
        private readonly IHotelService _hotelService;
        private readonly IFoodMenuService _foodMenuService;
        private readonly IMapper _mapper;
        private readonly IManagementOfHotelService _managementOfHotelService;
        private readonly IDirectorsDepartmansService _directorsDepartmansService;
        public FoodMenuDetailController(IMapper mapper, IDirectorsDepartmansService directorsDepartmansService, IManagementOfHotelService managementOfHotelService, IFoodMenuService foodMenuService, IHotelService hotelService, IFoodMenuDetailService foodMenuDetailService)
        {
            _foodMenuDetailService = foodMenuDetailService;
            _hotelService = hotelService;
            _foodMenuService = foodMenuService;
            _mapper = mapper;
            _managementOfHotelService = managementOfHotelService;
            _directorsDepartmansService = directorsDepartmansService;
        }
        [HttpGet("{menuid}")]
        public IActionResult GetAllByMenuId([FromHeader] string Authorization,int menuid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var menu=_foodMenuService.GetById(menuid);
            var hotel = _hotelService.GetById(keys, menu.hotelid);
            if (keys.role >= 1 && keys.role <= 5 && (keys.companyid == hotel.Companyid))
            {
                var foodmenudetail = _foodMenuDetailService.GetAllByMenuId(menuid);
                return CreateActionResult(ResponseDto<List<FoodMenuDetailResponse>>.Success(foodmenudetail, 200));
            }
            return CreateActionResult(ResponseDto<NoContentResult>.Authorization());
        }
        [HttpPost]
        public IActionResult AddMenuDetail([FromHeader] string Authorization, FoodMenuDetailDtoAddOrUpdateRequest request)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var foodMenu = _foodMenuService.GetById(request.foodMenuId);
            var hotel = _hotelService.GetById(keys,foodMenu.hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
            {
                _foodMenuDetailService.Create(request);
                return CreateActionResult(ResponseDto<NoContentResult>.Success(200));
            }
            else if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId,hotel.id);
                _foodMenuDetailService.Create(request);
                return CreateActionResult(ResponseDto<NoContentResult>.Success(200));
            }
            else if (keys.role == 5)
            {
                _directorsDepartmansService.GetDirectoryByHotelIdAndId(hotel.id, keys.userId);
                _foodMenuDetailService.Create(request);
                return CreateActionResult(ResponseDto<NoContentResult>.Success(200));
            }
            else
                return CreateActionResult(ResponseDto<NoContentResult>.Authorization());

        }
        [HttpPost]
        public IActionResult UpdateMenuDetail([FromHeader] string Authorization, FoodMenuDetailDtoAddOrUpdateRequest request)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var foodMenu = _foodMenuService.GetById(request.foodMenuId);
            var hotel = _hotelService.GetById(keys, foodMenu.hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
            {
                _foodMenuDetailService.Update(request);
                return CreateActionResult(ResponseDto<NoContentResult>.Success(200));
            }
            else if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, hotel.id);
                _foodMenuDetailService.Update(request);
                return CreateActionResult(ResponseDto<NoContentResult>.Success(200));
            }
            else if (keys.role == 5)
            {
                _directorsDepartmansService.GetDirectoryByHotelIdAndId(hotel.id, keys.userId);
                _foodMenuDetailService.Update(request);
                return CreateActionResult(ResponseDto<NoContentResult>.Success(200));
            }
            else
                return CreateActionResult(ResponseDto<NoContentResult>.Authorization());
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromHeader] string Authorization, int id)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var foodmenudetail = _foodMenuDetailService.GetByDetailId(id);
            var foodmenu = _foodMenuService.GetById(foodmenudetail.FoodMenuId);
            var hotel = _hotelService.GetById(keys, foodmenu.hotelid);
            if (keys.role >= 1 && keys.role <= 5 && (keys.companyid == hotel.Companyid))
            {
                return CreateActionResult(ResponseDto<FoodMenuDetailResponse>.Success(foodmenudetail, 200));
            }
            return CreateActionResult(ResponseDto<NoContentResult>.Authorization());
        }
    }
}
