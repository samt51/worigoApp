using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.Employee.Request;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.ManagerDto.Dto;
using Worigo.Core.Dtos.ManagerDto.Response;
using Worigo.Core.Dtos.User.Dto;
using Worigo.Core.Dtos.User.Response;
using Worigo.Core.Encryption;
using Worigo.Core.Exceptions;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfUserDal : EfRepositoryDal<User, DataContext>, IUserDal
    {
        public List<UserResponse> GetAllJoin()
        {
            using (var db = new DataContext())
            {
                var joinsorgu = from d1 in db.Users.Where(x => x.isDeleted == false)
                                join d2 in db.UserRole on d1.roleid equals d2.id
                                join d3 in db.Companies on d1.companyid equals d3.id
                                select new UserResponse
                                {
                                    id = d1.id,
                                    email = d1.email,
                                    roleid = d2.id,
                                    roleName = d2.RoleName,
                                    password = CommodMethods.ConvertDecrypt(d1.password),
                                    companyid = d3.id,
                                    companyName = d3.name,
                                };
                return joinsorgu.ToList();
            }
        }

        public List<EmployeesAndUserListDto> GetEmployeesByHotelid(int hotelid, int roleid)
        {
            using (var db = new DataContext())
            {

                var listmanagement = from d1 in db.Employees.Where(x => x.hotelid == hotelid && x.isDeleted == false)
                                     join d2 in db.Users on d1.userid equals d2.id
                                     join d3 in db.Departman on d1.employeestypeid equals d3.Id
                                     join d4 in db.Hotel on d1.hotelid equals d4.id
                                     join d5 in db.Companies on d4.Companyid equals d5.id
                                     join d6 in db.employeesType on d1.employeestypeid equals d6.id
                                     select new EmployeesAndUserListDto
                                     {
                                         id = d1.id,
                                         Name = d1.Name,
                                         Surname = d1.Surname,
                                         StartDateOfWork = d1.StartDateOfWork,
                                         email = d2.email,
                                         employeestypename = d6.TypeName,
                                         ExitEntryDate = d1.ExitEntryDate,
                                         FloorNo = d1.FloorNo,
                                         gender = d1.gender,
                                         hotel = d4.HotelName,
                                         companiesname = d5.name,
                                         ImageUrl = d1.ImageUrl,
                                         password = CommodMethods.ConvertDecrypt(d2.password),
                                         phoneNumber = d1.phoneNumber
                                     };
                return listmanagement.ToList();
            }
        }

        public ManagementUserResponse GetHotelAdminByAdminUserId(int adminUserId)
        {
            var userData = GetById(adminUserId);
            using (var db = new DataContext())
            {
                var join = from d1 in db.Employees.Where(x => x.userid == adminUserId)
                           join d2 in db.Companies on userData.companyid equals d2.id
                           select new ManagementUserResponse
                           {
                               Id = adminUserId,
                               name = d1.Name,
                               surname = d1.Surname,
                               CompanyName = d2.name,
                               email = userData.email,
                               password = CommodMethods.ConvertDecrypt(userData.password),
                               gender = d1.gender,
                               imageurl = d1.ImageUrl,
                               phonenumber = d1.phoneNumber
                           };
                var data = join.FirstOrDefault();
                return data;
            }
        }

        public List<ManagementUserResponse> GetHotelAdminByCompaniesId(int companiesid)
        {
            using (var db = new DataContext())
            {
                var joinlist = from d1 in db.Users.Where(x => x.companyid == companiesid && x.isDeleted == false && x.roleid == 2)
                               join d2 in db.Employees on d1.id equals d2.userid
                               join d3 in db.employeesType on d2.employeestypeid equals d3.id
                               join d4 in db.Companies on d1.companyid equals d4.id
                               where d1.isActive == true && d1.isDeleted == false && d2.isActive == true && d2.isDeleted == false && d3.isDeleted == false && d3.isActive == true && d4.isActive == true && d4.isDeleted == false
                               select new ManagementUserResponse
                               {
                                   Id = d1.id,
                                   name = d2.Name,
                                   surname = d2.Surname,
                                   email = d1.email,
                                   gender = d2.gender,
                                   imageurl = d2.ImageUrl,
                                   password = CommodMethods.ConvertDecrypt(d1.password),
                                   phonenumber = d2.phoneNumber,
                                   CompanyName = d4.name
                               };
                return joinlist.ToList();
            }
        }

        public List<ManagementResponse> GetManagemetByCompaniesid(int companiesid)
        {
            using (var db = new DataContext())
            {
                var response = new List<ManagementResponse>();
                var joinlist = from d1 in db.Users.Where(x => x.isDeleted == false && x.companyid == companiesid && x.roleid == 3)
                               join d2 in db.Employees on d1.id equals d2.userid
                               join d3 in db.ManagementOfHotels on d1.id equals d3.managementid
                               join d4 in db.Companies on d1.companyid equals d4.id
                               join d5 in db.employeesType on d2.employeestypeid equals d5.id
                               select new ManagementResponse
                               {
                                   ManagementId = d1.id,
                                   email = d1.email,
                                   password = CommodMethods.ConvertDecrypt(d1.password),
                                   gender = d2.gender,
                                   surname = d2.Surname,
                                   name = d2.Name,
                                   imageurl = d2.ImageUrl,
                                   phonenumber = d2.phoneNumber,
                                   StartDateOfWork = d2.StartDateOfWork,
                                   ExitEntryDate = d2.ExitEntryDate,
                                   companiesname = d4.name,
                                   employeestypename = db.Hotel.Where(x => x.id == d3.hotelid).FirstOrDefault().HotelName + " " + d5.TypeName
                               };
                var listmanagement = joinlist.ToList();

                var hotelEmployee = from manager in joinlist
                                    join managerhotel in db.ManagementOfHotels.Where(x => x.isDeleted == false && x.isActive == true) on manager.ManagementId equals managerhotel.managementid
                                    join hotel in db.Hotel on managerhotel.hotelid equals hotel.id
                                    select new ManagementResponseHotelResponse { HotelId = hotel.id, HotelName = hotel.HotelName, ManagerUserId = managerhotel.managementid };

                for (int i = 0; i < listmanagement.Count; i++)
                {
                    listmanagement[i].HotelList = hotelEmployee.Where(x => x.ManagerUserId == listmanagement[i].ManagementId).ToList();
                    response.Add(listmanagement[i]);
                }
                return joinlist.ToList();
            }
        }

        public List<ManagementUserResponse> GetManagemetByHotelid(int hotelid)
        {
            using (var db = new DataContext())
            {
                var listmanagement = from d1 in db.Hotel.Where(x => x.id == hotelid && x.isDeleted == false && x.isActive == true)
                                     join d2 in db.Users.Where(x => x.roleid == 3 && x.isDeleted == false && x.isActive == true) on d1.Companyid equals d2.companyid
                                     join d3 in db.Employees on d2.id equals d3.userid
                                     join d4 in db.Companies on d1.Companyid equals d4.id
                                     join d5 in db.employeesType on d3.employeestypeid equals d5.id
                                     select new ManagementUserResponse
                                     {
                                         Id = d2.id,
                                         name = d3.Name,
                                         surname = d3.Surname,
                                         phonenumber = d3.phoneNumber,
                                         StartDateOfWork = d3.StartDateOfWork,
                                         ExitEntryDate = d3.ExitEntryDate,
                                         CompanyName = d4.name,
                                         EmployeeTypeName = d5.TypeName,
                                         employeestypeid = d5.id,
                                         gender = d3.gender,
                                         email = d2.email,
                                         imageurl = d3.ImageUrl,
                                         password = CommodMethods.ConvertDecrypt(d2.password),
                                         companyId = d4.id
                                     };
                var response = listmanagement.ToList();

                return response;
            }
        }

        public User GetUserByEmail(string email)
        {
            using (var db = new DataContext())
            {
                var user = db.Users.Where(x => x.email == email).FirstOrDefault();
                if (user != null)
                    return user;
                throw new ClientSideException("User Not Found");
            }
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            using (var db = new DataContext())
            {
                return db.Users.Where(x => x.email == email && x.password == password && x.isDeleted == false).FirstOrDefault();
            }
        }

        public List<UserResponse> GetUserByHotelid(int hotelid)
        {
            using (var db = new DataContext())
            {
                var joinsorgu = from d1 in db.Users.Where(x => x.isDeleted == false && x.roleid > 2)
                                join d2 in db.UserRole on d1.roleid equals d2.id
                                join d3 in db.Companies on d1.companyid equals d3.id
                                select new UserResponse
                                {
                                    id = d1.id,
                                    email = d1.email,
                                    roleid = d2.id,
                                    roleName = d2.RoleName,
                                    password = CommodMethods.ConvertDecrypt(d1.password),
                                    companyid = d3.id,
                                    companyName = d3.name,
                                };
                return joinsorgu.ToList();
            }
        }


        public UserResponse UserGetByIdJoin(int id)
        {
            using (var db = new DataContext())
            {
                var listmanagement = from d1 in db.Users.Where(x => x.id == id && x.isDeleted == false)
                                     join d2 in db.UserRole on d1.roleid equals d2.id
                                     join d3 in db.Companies on d1.companyid equals d3.id
                                     select new UserResponse
                                     {
                                         id = d1.id,
                                         email = d1.email,
                                         roleid = d2.id,
                                         roleName = d2.RoleName,
                                         password = CommodMethods.ConvertDecrypt(d1.password),
                                         companyid = d3.id,
                                         companyName = d3.name,
                                     };
                return listmanagement.FirstOrDefault();
            }
        }
    }
}
