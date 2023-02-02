using AutoMapper;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Exceptions;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class RoomManager : IRoomService
    {
        private readonly IRoomDal _roomsDal;
        private readonly IHotelService _hotelService;
        private readonly IMapper _mapper;
        private readonly IManagementOfHotelService _managementOfHotelService;
        public RoomManager(IHotelService hotelService, IRoomDal roomsDal, IManagementOfHotelService managementOfHotelService, IMapper mapper)
        {
            _roomsDal = roomsDal;
            _mapper = mapper;
            _hotelService = hotelService;
            _managementOfHotelService = managementOfHotelService;
        }
        public ResponseDto<RoomDto> Create(RoomDto entity, TokenKeys keys)
        {
            var hotel = _hotelService.GetById(keys, entity.hotelid);
            if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, entity.hotelid);
                return new ResponseDto<RoomDto>().Success(200);
            }
            else if ((keys.companyid == hotel.data.Companyid) && keys.role == 2 || keys.role == 1)
            {
                var create = _roomsDal.Create(_mapper.Map<Room>(entity));
                return new ResponseDto<RoomDto>().Success(200);
            }
            return new ResponseDto<RoomDto>().Authorization();
        }

        public List<Room> GetAll()
        {
            return _roomsDal.GetAll(x => x.isDeleted == false);
        }

        public Room GetById(TokenKeys data, int id)
        {
            var room = _roomsDal.GetById(id);
            if (data.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(data.userId, room.hotelid);
                return room;
            }
            else if (data.role == 2 && data.companyid == _hotelService.GetById(data, room.hotelid).data.Companyid)
                return room;
            else if (data.role == 1)
                return room;
            throw new AuthorizationException("Authorization Failed");
        }

        public ResponseDto<RoomListJoin> roomGetByIdJoin(int id, TokenKeys keys)
        {
            var data = GetById(keys, id);
            var hotel = _hotelService.GetById(keys, data.hotelid);
            if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, data.hotelid);
                return new ResponseDto<RoomListJoin>().Success(_roomsDal.roomGetByIdJoin(id), 200);
            }
            else if (keys.role == 2 && keys.companyid == hotel.data.Companyid)
                return new ResponseDto<RoomListJoin>().Success(_roomsDal.roomGetByIdJoin(id), 200);
            else if (keys.role == 1)
                return new ResponseDto<RoomListJoin>().Success(_roomsDal.roomGetByIdJoin(id), 200);
            else
                return new ResponseDto<RoomListJoin>().Authorization();

        }

        public ResponseDto<List<RoomListJoin>> roomListJoins(TokenKeys data, int hotelid)
        {
            var hotel = _hotelService.GetById(data, hotelid);
            if (data.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(data.userId, hotelid);
                return new ResponseDto<List<RoomListJoin>>().Success(_roomsDal.roomListJoins(hotelid), 200);
            }
            else if (data.role == 2 && data.companyid == hotel.data.Companyid)
                return new ResponseDto<List<RoomListJoin>>().Success(_roomsDal.roomListJoins(hotelid), 200);
            else if (data.role == 1)
                return new ResponseDto<List<RoomListJoin>>().Success(_roomsDal.roomListJoins(hotelid), 200);
            else
                return new ResponseDto<List<RoomListJoin>>().Authorization();


        }

        public ResponseDto<List<RoomListJoin>> TakeFullOrEmptyToRooms(int hotelid, int type, TokenKeys keys)
        {
            var hotel = _hotelService.GetById(keys, hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.data.Companyid) || keys.role == 1)
            {
                return new ResponseDto<List<RoomListJoin>>().Success(_roomsDal.TakeFullOrEmptyToRooms(hotelid, type), 200);
            }
            else if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                return new ResponseDto<List<RoomListJoin>>().Success(_roomsDal.TakeFullOrEmptyToRooms(hotelid, type), 200);
            }
            return new ResponseDto<List<RoomListJoin>>().Authorization();
        }

        public ResponseDto<RoomDto> Update(RoomDto entity, TokenKeys keys)
        {
            var roomdata = GetById(keys, entity.id);
            var hotel = _hotelService.GetById(keys, entity.hotelid);
            if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, roomdata.hotelid);
                roomdata.ModifyDate = System.DateTime.Now;
                roomdata.Price = entity.Price;
                roomdata.RoomNo = entity.RoomNo;
                roomdata.NumberOfBeds = entity.NumberOfBeds;
                _roomsDal.Update(roomdata);
                return new ResponseDto<RoomDto>().Success(200);


            }
            if ((keys.companyid == hotel.data.Companyid) && keys.role == 2 || keys.role == 1)
            {
                roomdata.ModifyDate = System.DateTime.Now;
                roomdata.Price = entity.Price;
                roomdata.RoomNo = entity.RoomNo;
                roomdata.NumberOfBeds = entity.NumberOfBeds;
                _roomsDal.Update(roomdata);
                return new ResponseDto<RoomDto>().Success(200);
            }
            return new ResponseDto<RoomDto>().Authorization();
        }
    }
}
