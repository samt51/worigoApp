using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Worigo.API.Model.UserViewModel;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.Room.Response;
using Worigo.Core.Enum;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class RoomController : CustomBaseController
    {
        private readonly IRoomService _roomsService;
        private readonly IManagementOfHotelService _managementOfHotelService;
        private readonly IMapper _mapper;
        private readonly IHotelService _hotelService;
        public RoomController(IHotelService hotelService, IManagementOfHotelService managementOfHotelService, IRoomService roomsService, IMapper mapper)
        {
            _roomsService = roomsService;
            _mapper = mapper;
            _managementOfHotelService = managementOfHotelService;
            _hotelService = hotelService;
        }
        [HttpGet("{hotelid}")]
        public ResponseDto<List<RoomListJoin>> GetAllByHotelid([FromHeader] string Authorization, int hotelid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _roomsService.roomListJoins(keys, hotelid);
        }
        [HttpGet("{id}")]
        public ResponseDto<RoomListJoin> GetById([FromHeader] string Authorization, int id)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _roomsService.roomGetByIdJoin(id, keys);
        }
        [HttpPost("{hotelid}")]
        public ResponseDto<RoomDto> Add([FromHeader] string Authorization, RoomDto entity)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _roomsService.Create(entity, keys);
        }
        [HttpPost("{id}")]
        public IActionResult Delete([FromHeader] string Authorization, int id)
        {

            return null;
        }
        [HttpPost]
        public ResponseDto<RoomDto> Update([FromHeader] string Authorization, RoomDto entity)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _roomsService.Update(entity, keys);
        }
        [HttpGet("{hotelId}/{type}")]
        //type 2 = boş odaları 1=dolu odaları
        public ResponseDto<List<RoomListJoin>> TakeRoomEmptyOrFull([FromHeader] string Authorization, int hotelId, int type)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _roomsService.TakeFullOrEmptyToRooms(hotelId, type, keys);
        }
    }
}
