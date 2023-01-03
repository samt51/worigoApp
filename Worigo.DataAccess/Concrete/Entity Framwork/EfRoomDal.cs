using System;
using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.Room.Response;
using Worigo.Core.Enum;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfRoomDal : EfRepositoryDal<Room, DataContext>, IRoomDal
    {
        public RoomListJoin roomGetByIdJoin(int id)
        {
            using (var db = new DataContext())
            {
                var joingetbyid = from d1 in db.Room.Where(x => x.id == id && x.isDeleted == false)
                                  join d2 in db.RoomType on d1.RoomTypeid equals d2.id
                                  join d3 in db.Hotel on d1.hotelid equals d3.id
                                  join d4 in db.Companies on d3.Companyid equals d4.id
                                  select new RoomListJoin
                                  {
                                      Description = d1.Description,
                                      Hotel = d3.Adress,
                                      NumberOfBeds = d1.NumberOfBeds,
                                      RoomTypeid = d1.RoomTypeid,
                                      Price = d1.Price,
                                      RoomNo = d1.RoomNo,
                                      RoomType = d2.typeName,
                                      id = d1.id,
                                      Company = d4.name,
                                      IsFull = d1.isActive
                                  };
                return joingetbyid.First();
            }
        }

        public List<RoomListJoin> roomListJoins(int hotelid)
        {
            using (var db = new DataContext())
            {

                var joinlist = from d1 in db.Room.Where(x => x.isDeleted == false && x.hotelid == hotelid)
                               join d2 in db.RoomType on d1.RoomTypeid equals d2.id
                               join d3 in db.Hotel on d1.hotelid equals d3.id
                               join d4 in db.Companies on d3.Companyid equals d4.id
                               select new RoomListJoin
                               {
                                   Description = d1.Description,
                                   Hotel = d3.HotelName,
                                   id = d1.id,
                                   NumberOfBeds = d1.NumberOfBeds,
                                   Price = d1.Price,
                                   RoomNo = d1.RoomNo,
                                   RoomTypeid = d1.RoomTypeid,
                                   RoomType = d2.typeName,
                                   Company = d4.name,
                                   IsFull = d1.isActive
                               };
                return joinlist.ToList();
            }
        }

        public List<RoomListJoin> TakeFullOrEmptyToRooms(int hotelid, int type)
        {
            var response = new List<RoomListJoin>();
            using (var db = new DataContext())
            {
                if (type == 2)
                {
                    var join = from d1 in db.Hotel
                               where d1.id == hotelid && d1.isDeleted == false && d1.isActive == true
                               join d2 in db.Room on d1.id equals d2.hotelid
                               where d2.isDeleted == false && d2.isActive == true && d2.IsFull == false
                               join d3 in db.RoomType on d2.RoomTypeid equals d3.id
                               where d3.isActive == true && d3.isDeleted == false
                               join d4 in db.Companies on d1.Companyid equals d4.id
                               select new RoomListJoin
                               {
                                   Hotel = d1.HotelName,

                                   RoomNo = d2.RoomNo,
                                   id = d2.id,
                                   Company = d4.name,
                                   Description = d2.Description,
                                   IsFull = d2.IsFull,
                                   NumberOfBeds = d2.NumberOfBeds,
                                   Price = d2.Price,
                                   RoomType = d3.typeName,
                                   RoomTypeid = d3.id,
                               };
                    response = join.ToList();
                }
                else if (type == 1)
                {
                    var join = from d1 in db.Hotel
                               where d1.id == hotelid && d1.isDeleted == false && d1.isActive == true
                               join d2 in db.Room on d1.id equals d2.hotelid
                               where d2.isDeleted == false && d2.isActive == true && d2.IsFull == true
                               join d3 in db.vertificationCodes on d2.id equals d3.roomid
                               where d3.IsFull == true
                               join d4 in db.Companies on d1.Companyid equals d4.id
                               join d5 in db.RoomType on d2.RoomTypeid equals d5.id
                               where d5.isActive == true && d3.isDeleted == false
                               select new RoomListJoin
                               {
                                   Hotel = d1.HotelName,
                                   hotelid = d1.id,
                                   RoomNo = d2.RoomNo,
                                   id = d2.id,
                                   startDate = d3.StartDate,
                                   endDate = d3.FinishDate,
                                   daily = Math.Abs(d3.StartDate.Day - d3.FinishDate.Day),
                                   Company = d4.name,
                                   Description = d2.Description,
                                   IsFull = d2.IsFull,
                                   Price = d2.Price,
                                   RoomTypeid = d5.id,
                                   RoomType = d5.typeName,
                                   NumberOfBeds = d2.NumberOfBeds,
                              
                               };
                    response = join.ToList();
                }
                return response;
            }
        }
    }
}
