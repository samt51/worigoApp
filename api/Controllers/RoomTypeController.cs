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
        public IActionResult GetAll([FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var list = _roomTypeService.GetAll();
            var listdto = _mapper.Map<List<RoomTypeDto>>(list);
            return CreateActionResult(ResponseDto<List<RoomTypeDto>>.Success(listdto, 200));
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromHeader] string Authorization, int id)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var entityid = _roomTypeService.GetById(id);
            var singulardto = _mapper.Map<RoomTypeDto>(entityid);
            return CreateActionResult(ResponseDto<RoomTypeDto>.Success(singulardto, 200));
        }
        [HttpPost]
        public IActionResult Add([FromHeader] string Authorization, RoomTypeDto roomTypeDto)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if(keys.role==1)
            {
                var entity = new RoomType
                {
                    CreatedDate = System.DateTime.Now,
                    isActive =true,
                    isDeleted = false,
                    ModifyDate = System.DateTime.Now,
                    typeName = roomTypeDto.typeName
                };
                _roomTypeService.Create(entity);
                return CreateActionResult(ResponseDto<RoomType>.Success(200));
            }
            return CreateActionResult(ResponseDto<List<EmployeeResponse>>.Authorization());

        }
        [HttpPost]
        public IActionResult Update([FromHeader] string Authorization, RoomTypeDto roomTypeDto)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys.role == 1)
            {
                var entity = _roomTypeService.GetById(roomTypeDto.id);
                entity.ModifyDate = System.DateTime.Now;
                entity.typeName = roomTypeDto.typeName;
                _roomTypeService.Update(entity);
                return CreateActionResult(ResponseDto<RoomType>.Success(200));
            }
            return CreateActionResult(ResponseDto<List<RoomType>>.Authorization());
        }
        [HttpPost("{id}")]
        public IActionResult Delete([FromHeader] string Authorization, int id)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys.role == 1)
            {
                var entity = _roomTypeService.GetById(id);
                entity.isDeleted = true;
                entity.ModifyDate = System.DateTime.Now;
                _roomTypeService.Update(entity);
                return CreateActionResult(ResponseDto<RoomType>.Success(200));
            }
            return CreateActionResult(ResponseDto<List<RoomType>>.Authorization());

        }
    }
}
