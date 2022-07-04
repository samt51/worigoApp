using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RoomController : CustomBaseController
    {
        private readonly IRoomService _roomsService;
        private readonly IMapper _mapper;
        public RoomController(IRoomService roomsService, IMapper  mapper)
        {
            _roomsService = roomsService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll(int hotelid)
        {
            var rooms = _roomsService.roomListJoins(hotelid);
             
            return CreateActionResult(ResponseDto<List<RoomListJoin>>.Success(rooms, 200));
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var roomSingular = _roomsService.GetById(id);
            var roomDto = _mapper.Map<RoomDto>(roomSingular);
            return CreateActionResult(ResponseDto<RoomDto>.Success(roomDto, 200));
        }
        [HttpPost]
        public IActionResult Add(RoomDto entity)
        {
            var entitydto = new Room
            {
                isDeleted=false,
                CreatedDate=System.DateTime.Now,
                ModifyDate=System.DateTime.Now,
                Description=entity.Description,
                NumberOfBeds=entity.NumberOfBeds,
                Price=entity.Price,
                hotelid=entity.hotelid,
                isActive=entity.isActive,
                RoomTypeid=entity.RoomTypeid,
                RoomNo=entity.RoomNo,
                
            };
            _roomsService.Create(_mapper.Map<Room>(entitydto));
            return CreateActionResult(ResponseDto<Room>.Success(200));
        }
        [HttpPost("{id}")]
        public IActionResult Delete(int id)
        {
            var campaigndelete = _roomsService.GetById(id);
            campaigndelete.isDeleted = true;
            _roomsService.Update(campaigndelete);
            return CreateActionResult(ResponseDto<Room>.Success(200));
        }
        [HttpPost]
        public IActionResult Update(RoomDto campaign)
        {
            var roomdata = _roomsService.GetById(campaign.id);
            roomdata.ModifyDate = System.DateTime.Now;
            roomdata.Price = campaign.Price;
            roomdata.RoomNo = campaign.RoomNo;
            roomdata.NumberOfBeds = campaign.NumberOfBeds;
            roomdata.isActive = campaign.isActive;
            roomdata.RoomTypeid = campaign.RoomTypeid;
            _roomsService.Update(roomdata);
            return CreateActionResult(ResponseDto<Room>.Success(200));
        }
    }
}
