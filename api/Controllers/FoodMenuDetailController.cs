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
        public ResponseDto<List<FoodMenuDetailResponse>> GetAllByMenuId([FromHeader] string Authorization, int menuid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _foodMenuDetailService.GetAllByMenuId(menuid, keys);
        }
        [HttpPost]
        public ResponseDto<FoodMenuDetailResponse> AddMenuDetail([FromHeader] string Authorization, FoodMenuDetailDtoAddOrUpdateRequest request)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _foodMenuDetailService.Create(request, keys);
        }
        [HttpPost]
        public ResponseDto<FoodMenuDetailResponse> UpdateMenuDetail([FromHeader] string Authorization, FoodMenuDetailDtoAddOrUpdateRequest request)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _foodMenuDetailService.Update(request, keys);
        }
        [HttpGet("{id}")]
        public ResponseDto<FoodMenuDetailResponse> GetById([FromHeader] string Authorization, int id)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _foodMenuDetailService.GetByDetailId(id, keys);
        }
    }
}
