using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class RoomManager : IRoomService
    {
        private readonly IRoomDal _roomsDal;
        public RoomManager(IRoomDal roomsDal)
        {
            _roomsDal = roomsDal;
        }
        public void Create(Room entity)
        {
            _roomsDal.Create(entity);
        }

        public List<Room> GetAll()
        {
            return _roomsDal.GetAll(x => x.isDeleted == false);
        }

        public Room GetById(int id)
        {
           return _roomsDal.GetById(id);
        }

        public List<RoomListJoin> roomListJoins(int hotelid)
        {
            return _roomsDal.roomListJoins(hotelid);
        }

        public void Update(Room entity)
        {
            _roomsDal.Update(entity);
        }
    }
}
