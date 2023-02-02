using System.Linq;
using Worigo.Core.Dtos.DirectorsDepartmans.Dto;
using Worigo.Core.Dtos.DirectorsDepartmans.Request;
using Worigo.Core.Dtos.DirectorsDepartmans.Response;
using Worigo.Core.Encryption;
using Worigo.Core.Exceptions;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfDirectorsDepartmansDal : EfRepositoryDal<DirectorsDepartmans, DataContext>, IDirectorsDepartmansDal
    {
        public DirectorsDepartmans GetDirectoryByHotelIdAndId(int hotelid, int id)
        {
            using (var db=new DataContext())
            {
                var entity = db.DirectorsDepartmans.Where(x => x.hotelid == hotelid && x.directoryid == id && x.isDeleted == false).FirstOrDefault();
                if (entity == null)
                {
                    throw new ClientSideException($"{typeof(DirectorsDepartmans).Name}  Not Found");
                }
                return entity;
            }
        }

        public UserAndDirectoryResponse GetDirectoryByUserId(int directoryUserId)
        {
            using (var db=new DataContext())
            {
                var joinData = from d1 in db.Employees.Where(x => x.isActive == true && x.isDeleted == false&&x.id==directoryUserId)
                               join d2 in db.Users on d1.userid equals d2.id
                               join d3 in db.UserRole on d2.roleid equals d3.id
                               select new UserAndDirectoryResponse
                               {
                                   DirectoryEmployeeId = directoryUserId,
                                   employeeid=d1.id,
                                   name=d1.Name,
                                   surname=d1.Surname,
                                   StartDateOfWork=d1.StartDateOfWork,
                                   ExitEntryDate=d1.ExitEntryDate,
                                   email=d2.email,
                                   password=CommodMethods.ConvertDecrypt(d2.password),
                                   gender=d1.gender,
                                   imageurl=d1.ImageUrl,
                                   phonenumber=d1.phoneNumber
                               };
                var dataFirst = joinData.FirstOrDefault();

                var hotelEmployee = from d1 in joinData
                                    join d2 in db.DirectorsDepartmans on d1.DirectoryEmployeeId equals d2.directoryid
                                  
                                    join d3 in db.Departman on d2.departmanid equals d3.Id
                                    select new DirectoryDepartmentResponseAllDepartmentResponse
                                    {
                                        departmanid=d3.Id,
                                        DepartmentName=d3.DepartmanName,
                                        directoryEmployeeId=d2.directoryid
                                    };
                joinData.First().AllDepartment = hotelEmployee.ToList();
                return joinData.First();
                                   
            }
        }

        public void ToDepartmentManagerRemove(UserAndDirectoryDepartmentAddOrUpdateRequest request)
        {
            using (var db=new DataContext())
            {
                GetDirectoryByHotelIdAndId(request.hotelid, (int)request.directoryEmployeeId);
                //var departmentRemove = db.DirectorsDepartmans.Where(x => x.departmanid == request.currentDepartmentId && x.directoryid == request.directoryEmployeeId).FirstOrDefault();
                //if (departmentRemove == null)
                //    throw new NotFoundException("This Manager Is Not Affiliated With This Department");
                //departmentRemove.isActive = false;
                //departmentRemove.isDeleted = true;
                //var departmentUpdate= Update(departmentRemove);
            }
        }
    }
}
