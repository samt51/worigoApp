using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
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
        public IActionResult GetAll()
        {
            var list = _roomTypeService.GetAll();
            var listdto = _mapper.Map<List<RoomTypeDto>>(list);
            return CreateActionResult(ResponseDto<List<RoomTypeDto>>.Success(listdto, 200));
        }  
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var entityid = _roomTypeService.GetById(id);
            var singulardto = _mapper.Map<RoomTypeDto>(entityid);
            return CreateActionResult(ResponseDto<RoomTypeDto>.Success(singulardto, 200));
        }
        [HttpPost]
        public IActionResult Add(RoomTypeDto roomTypeDto)
        {
            var entity = new RoomType
            {
                CreatedDate = System.DateTime.Now,
                isActive = roomTypeDto.isActive,
                isDeleted = false,
                ModifyDate = System.DateTime.Now,
                typeName = roomTypeDto.typeName
            };
            _roomTypeService.Create(entity);
            return CreateActionResult(ResponseDto<RoomType>.Success(200));
        }
        [HttpPost]
        public IActionResult Update(RoomTypeDto roomTypeDto)
        {
            var entity = _roomTypeService.GetById(roomTypeDto.id);
            entity.isActive = roomTypeDto.isActive;
            entity.ModifyDate = System.DateTime.Now;
            entity.typeName = roomTypeDto.typeName;
            _roomTypeService.Update(entity);
            return CreateActionResult(ResponseDto<RoomType>.Success(200));
        }
        [HttpPost("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _roomTypeService.GetById(id);
            entity.isDeleted = true;
            entity.ModifyDate = System.DateTime.Now;
            _roomTypeService.Update(entity);
            return CreateActionResult(ResponseDto<RoomType>.Success(200));
        }
    }
}
