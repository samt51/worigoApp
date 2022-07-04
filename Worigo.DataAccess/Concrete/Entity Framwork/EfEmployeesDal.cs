using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.JoinClass;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfEmployeesDal : EfRepositoryDal<Employees, DataContext>, IEmployeesDal
    {
        public List<EmployeesListJoin> employeesListJoins(int hotelid)
        {
            using (var db = new DataContext())
            {
                var joinlist = from d1 in db.Employees.Where(x => x.hotelid == hotelid)
                               join d2 in db.Users on d1.userid equals d2.id
                               join d3 in db.Departman on d1.departmanid equals d3.Id
                               join d4 in db.Hotel on d1.hotelid equals d4.id
                               select new EmployeesListJoin
                               {
                                   id = d1.id,
                                   StartDateOfWork = d1.StartDateOfWork,
                                   phoneNumber = d1.phoneNumber,
                                   Surname = d1.Surname,
                                   departman = d3.DepartmanName,
                                   ExitEntryDate = d1.ExitEntryDate,
                                   FloorNo = d1.FloorNo,
                                   gender = d1.gender,
                                   hotel = d4.HotelName,
                                   ImageUrl = d1.ImageUrl,
                                   user = d2.email
                               };
                return joinlist.ToList();
            }
        }
    }
}
