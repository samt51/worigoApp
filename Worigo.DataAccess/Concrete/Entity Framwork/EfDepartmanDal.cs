using System;
using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.Departman.Response;
using Worigo.Core.Exceptions;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfDepartmanDal : EfRepositoryDal<Departman, DataContext>, IDepartmanDal
    {
        public DepartmentCommentRateResponse DepartmanCommentRateResponse(int hotelid, int departmanid)
        {
            using (var db = new DataContext())
            {
                var hotelList = db.Hotel.Where(x => x.isDeleted == false && x.id == hotelid).FirstOrDefault();
                var CommentList = db.Comment.Where(x => x.isDeleted == false).ToList();
                var EmployeeList = db.Employees.Where(x => x.isDeleted == false&&x.hotelid==hotelid).ToList();

                var employeetypelist = db.employeesType.Where(x => x.isDeleted == false && x.departmanid == departmanid).ToList();
                var employees = new List<Employees>();

                foreach (var item in employeetypelist)
                {
                    int? asd = item.id;
                    employees = EmployeeList.Where(x => x.employeestypeid == item.id).ToList();
                }
                int point = 0;
                int commetcount = 0;
                var hotelcommentList = new List<Comment>();
                for (int i = 0; i < employees.Count; i++)
                {
                    hotelcommentList = CommentList.Where(x => x.employeesid == employees.ToList()[i].id).ToList();
                    commetcount += hotelcommentList.Count;
                    for (int k = 0; k < hotelcommentList.Count; k++)
                    {
                        point += hotelcommentList[k].Point;

                    }
                }
                if (point == 0)
                {
                    var rate = new DepartmentCommentRateResponse
                    {
                        HotelName = hotelList.HotelName,
                        Departman = db.Departman.Where(x => x.Id == departmanid).FirstOrDefault().DepartmanName,
                        Rate = 0 + " " + "%"
                    };
                    return rate;
                }

                var ratesum =  Convert.ToDecimal(point) / Convert.ToDecimal(commetcount);
                if (ratesum >= 5)
                    ratesum = 5;
                var entity = new DepartmentCommentRateResponse
                {
                    HotelName = hotelList.HotelName,
                    Departman = db.Departman.Where(x => x.Id == departmanid).FirstOrDefault().DepartmanName,
                    Rate = ratesum.ToString() + " " + "%"
                };
                return entity;

            }
        }
    }
}
