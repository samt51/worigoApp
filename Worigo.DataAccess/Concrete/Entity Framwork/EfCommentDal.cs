using System;
using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.Comment.Response;
using Worigo.Core.Dtos.Reports.HotelGeneralPuan;
using Worigo.Core.Exceptions;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfCommentDal : EfRepositoryDal<Comment, DataContext>, ICommentDal
    {
        public CommentResponse GetByIdJoin(int id)
        {
            using (var db = new DataContext())
            {
                var joinsorgu = from d1 in db.Comment.Where(x => x.Id == id)
                                join d2 in db.Employees on d1.employeesid equals d2.id
                                join d3 in db.employeesType on d2.employeestypeid equals d3.id
                                join d4 in db.Hotel on d2.hotelid equals d4.id
                                select new CommentResponse
                                {
                                    Id = d1.Id,
                                    EmployeeNameAndSurname = d2.Name + " " + d2.Surname,
                                    Commentary = d1.Commentary,
                                    Point = d1.Point,
                                    EmployeesType = d3.TypeName,
                                    Hotel = d4.HotelName,
                                    hotelid = d4.id
                                };
                var data= joinsorgu.FirstOrDefault();
                return data;
            }
        }
        public List<CommentResponse> GetCommentByHotelid(int hotelid)
        {
            using (var db = new DataContext())
            {
                var joinList = from d1 in db.Comment.Where(x => x.hotelid == hotelid && x.isDeleted == false)
                               join d2 in db.Hotel on d1.hotelid equals d2.id
                               join d3 in db.Employees on d1.employeesid equals d3.id
                               join d4 in db.employeesType on d3.employeestypeid equals d4.id
                               select new CommentResponse
                               {
                                   Id = d1.Id,
                                   Commentary = d1.Commentary,
                                   EmployeeNameAndSurname = d3.Name + " " + d3.Surname,
                                   Hotel = d2.HotelName,
                                   EmployeesType = d4.TypeName,
                                   Point = d1.Point,
                                   contentsPoint = d1.contentsPoint,
                                   speedPoint = d1.speedPoint,
                                   CreateDate = d1.CreatedDate,
                                   GeneralPoint = (d1.Point + d1.contentsPoint + d1.speedPoint) / 3
                               };
                return joinList.OrderByDescending(x => x.CreateDate).ToList();
            }
        }
        public List<CommentResponse> GetEmployeesOfCommentByHotelidAndEmployeesid(int hotelid, int employeeid)
        {
            throw new System.NotImplementedException();
        }
        public HotelGeneralPointResponse HotelGeneralPointByHotelId(int hotelid)
        {
            using (var db = new DataContext())
            {
                var GeneralHotelPointList = db.Comment.Where(x => x.hotelid == hotelid && x.isDeleted == false).ToList();
                var HotelName = db.Hotel.Where(x => x.id == hotelid && x.isDeleted == false).FirstOrDefault();
                if (HotelName == null)
                    throw new ClientSideException("Hotel Not Found");
                var EmployeePoint = 0;
                var SpeedPoint = 0;
                var ServicePoint = 0;
                EmployeePoint = GeneralHotelPointList.Sum(x => x.Point);
                SpeedPoint = GeneralHotelPointList.Sum(x => x.speedPoint);
                ServicePoint = GeneralHotelPointList.Sum(x => x.contentsPoint);
                var employe = Convert.ToDecimal(Convert.ToDecimal(EmployeePoint / Convert.ToDecimal((GeneralHotelPointList.Count() * 3))).ToString("0.#"));
                var speed = Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(SpeedPoint) / GeneralHotelPointList.Select(x => x.speedPoint).Count()).ToString("0.#"));
                var service = Convert.ToDecimal((Convert.ToDecimal(ServicePoint) / GeneralHotelPointList.Count()).ToString("0.#"));
                var GeneralSum = Convert.ToDecimal((employe + speed + service) / GeneralHotelPointList.Count);
                var entity = new HotelGeneralPointResponse
                {
                    HotelName = HotelName.HotelName,
                    EmployeePoint = employe,
                    SpeedPoint = speed,
                    ServicePoint = service,
                    HotelAveragePoint = GeneralSum,
                    CommentCount = GeneralHotelPointList.Count
                };
                return entity;
            }
        }
    }
}
