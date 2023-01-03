using System;
using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.Reports.Response;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfVertificationCodeDal : EfRepositoryDal<VertificationCodes, DataContext>, IVertificationCodeDal
    {
        public VertificationCodes GetCodesByRoomid(int roomnumber)
        {
            throw new System.NotImplementedException();
        }

        public RoomCountResponse GetRoomCountByDate(int hotelid, DateTime date)
        {
            using (var db = new DataContext())
            {
                var allroomrate = db.Room.Where(x => x.hotelid == hotelid).ToList().Count;
                var list = db.vertificationCodes.Where(x => x.StartDate.Year == date.Year && x.StartDate.Month == date.Month && x.StartDate.Day == date.Day && x.hotelid == hotelid).ToList();
                var join = from j1 in list
                           join j2 in db.Hotel.ToList() on j1.hotelid equals j2.id
                           select new RoomCountResponse
                           {
                               HotelName = j2.HotelName,
                               RoomCount = list.Count
                           };
                return join.First();
            }
        }

        public GetRoomOccupancyRateByDateResponse GetRoomOccupancyRateByDate(int hotelid, DateTime startDate)
        {
            using (var db = new DataContext())
            {
                var date1 = new DateTime(startDate.Year, startDate.Month, startDate.Day, 00, 00, 00);

                var allroomrate = db.Room.Count(x=>x.hotelid==hotelid);
                var list = db.vertificationCodes.Where(x => x.StartDate.Year == startDate.Year && x.StartDate.Month == startDate.Month && x.StartDate.Day == startDate.Day && x.hotelid == hotelid).ToList();
                var join = from j1 in list
                           join j2 in db.Hotel.ToList() on j1.hotelid equals j2.id
                           select new GetRoomOccupancyRateByDateResponse
                           {
                               HotelName = j2.HotelName,
                               Occopancy = Convert.ToInt32((Convert.ToDecimal(list.Count) / Convert.ToDecimal(allroomrate)) * 100).ToString() + " " + "%"
                           };
                return join.First();
            }
        }

        public RoomCountResponse GetTotalRoomCountOfUsedApp(int hotelid)
        {
            using (var db = new DataContext())
            {
                var entity = new RoomCountResponse
                {
                    HotelName = db.Hotel.FirstOrDefault(x => x.id == hotelid).HotelName,
                    RoomCount = db.vertificationCodes.Where(x => x.hotelid == hotelid && x.isDeleted == false).ToList().Count
                };
                return entity;
            }
        }

        public RoomCountResponse GetTotalRoomCountOfUsedAppDateSearch(int hotelid, DateTime starDate, DateTime endDate)
        {
            using (var dB = new DataContext())
            {

                var data = dB.vertificationCodes.Where(x => x.hotelid == hotelid && x.StartDate >= starDate && x.StartDate <= endDate).ToList();
                var entityResponse = new RoomCountResponse
                {
                    HotelName = dB.Hotel.Where(x => x.id == hotelid).FirstOrDefault().HotelName,
                    RoomCount = data.Count
                };
                return entityResponse;
            }
        }
    }
}
