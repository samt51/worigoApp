using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.API.Model.UserViewModel;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.Employee.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.RoomType.Request;
using Worigo.Core.Dtos.RoomType.Response;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class RoomTypeController : CustomBaseController
    {
        private readonly IRoomTypeService _roomTypeService;
        private readonly IMapper _mapper;
        public RoomTypeController(IMapper mapper, IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
            _mapper = mapper;

        }
        [HttpGet]
        public ResponseDto<List<RoomTypeResponse>> GetAll([FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _roomTypeService.GetAll(keys);

        }
        [HttpGet("{id}")]
        public ResponseDto<RoomTypeResponse> GetById([FromHeader] string Authorization, int id)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _roomTypeService.GetById(id, keys);
        }
        [HttpPost]
        public ResponseDto<RoomTypeResponse> Add([FromHeader] string Authorization, RoomTypeAddOrUpdateRequest request)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _roomTypeService.Create(request, keys);

        }
        [HttpPost]
        public ResponseDto<RoomTypeResponse> Update([FromHeader] string Authorization, RoomTypeAddOrUpdateRequest request)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _roomTypeService.Update(request, keys);

        }
        //[HttpPost("{id}")]
        //public IActionResult Delete([FromHeader] string Authorization, int id)
        //{
        //    TokenKeys keys = AuthorizationCont.Authorization(Authorization);
        //    if (keys.role == 1)
        //    {
        //        var entity = _roomTypeService.GetById(id);
        //        entity.isDeleted = true;
        //        entity.ModifyDate = System.DateTime.Now;
        //        _roomTypeService.Update(entity);
        //        return CreateActionResult(ResponseDto<RoomType>.Success(200));
        //    }
        //    return CreateActionResult(ResponseDto<List<RoomType>>.Authorization());

        //}
    }
}
