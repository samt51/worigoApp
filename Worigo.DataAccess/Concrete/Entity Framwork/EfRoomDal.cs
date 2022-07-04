using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.JoinClass;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfRoomDal : EfRepositoryDal<Room, DataContext>, IRoomDal
    {
        public List<RoomListJoin> roomListJoins(int hotelid)
        {
            using (var db = new DataContext())
            {
                var joinlist = from d1 in db.Room.Where(x => x.isDeleted == false && x.hotelid == hotelid)
                               join d2 in db.RoomType on d1.RoomTypeid equals d2.id
                               join d3 in db.Hotel on d1.hotelid equals d3.id
                               select new RoomListJoin
                               {
                                   Description=d1.Description,
                                   Hotel=d3.HotelName,
                                   id=d1.id,
                                   NumberOfBeds=d1.NumberOfBeds,
                                   Price=d1.Price,
                                   RoomNo=d1.RoomNo,
                                   RoomType=d2.typeName
                               };
                return joinlist.ToList();
            }
        }
    }
}
