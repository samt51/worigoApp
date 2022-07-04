using System.Collections.Generic;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IRoomService
    {
        List<RoomListJoin> roomListJoins(int hotelid);
        List<Room> GetAll();
        Room GetById(int id);
        void Create(Room entity);
        void Update(Room entity);
    }
}
