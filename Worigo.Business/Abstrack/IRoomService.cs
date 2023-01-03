using System.Collections.Generic;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IRoomService
    {
        ResponseDto<RoomListJoin> roomGetByIdJoin(int id, TokenKeys keys);
        ResponseDto<List<RoomListJoin>> roomListJoins(TokenKeys data,int hotelid);
        List<Room> GetAll();
        Room GetById(TokenKeys data,int id);
        ResponseDto<RoomDto> Create(RoomDto entity, TokenKeys data);
        ResponseDto<RoomDto> Update(RoomDto entity, TokenKeys keys);
        ResponseDto<List<RoomListJoin>> TakeFullOrEmptyToRooms(int hotelid, int type, TokenKeys keys);
    }
}
