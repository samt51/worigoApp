using System;
using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.DirectorsDepartmans.Dto;
using Worigo.Core.Dtos.DirectorsDepartmans.Response;
using Worigo.Core.Dtos.Employee.Response;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.ManagerDto.Dto;
using Worigo.Core.Dtos.ManagerDto.Response;
using Worigo.Core.Dtos.User.Dto;
using Worigo.Core.Encryption;
using Worigo.Core.Exceptions;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;
using Worigo.Entity.Encryption;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfEmployeesDal : EfRepositoryDal<Employees, DataContext>, IEmployeesDal
    {
        public List<EmployeeResponse> GetEmployeesByHotelId(int hotelid)
        {
            using (var db = new DataContext())
            {
                var joinlist = from d1 in db.Employees.Where(x => x.hotelid == hotelid && x.isDeleted == false)
                               join d2 in db.Users on d1.userid equals d2.id
                               join d3 in db.employeesType on d1.employeestypeid equals d3.id
                               join d4 in db.Hotel on d1.hotelid equals d4.id
                               join d5 in db.Departman on d3.departmanid equals d5.Id
                               select new EmployeeResponse
                               {
                                   EmployeeId = d1.id,
                                   Name = d1.Name,
                                   Surname = d1.Surname,
                                   FloorNo = d1.FloorNo,
                                   gender = d1.gender,
                                   phoneNumber = d1.phoneNumber,
                                   StartDateOfWork = new DateTime(d1.StartDateOfWork.Value.Year, d1.StartDateOfWork.Value.Month, d1.StartDateOfWork.Value.Day),
                                   ExitEntryDate = d1.ExitEntryDate == null ? null : d1.ExitEntryDate != null ? new DateTime(d1.ExitEntryDate.Value.Year, d1.ExitEntryDate.Value.Month, d1.ExitEntryDate.Value.Day) : null,
                                   hotelid = d4.id,
                                   HotelName = d4.HotelName,
                                   Employeestypeid = d3.id,
                                   PositionName = d3.TypeName,
                                   DepartmentId = d5.Id,
                                   DepartmentName = d5.DepartmanName,
                                   ImageUrl = d1.ImageUrl,
                               };

                var list = joinlist.ToList();
                return list;
            }
        }
        public EmployeeResponse GetEmployeeByEmployeeId(int employeeId)
        {
            var data = GetById(employeeId);
            using (var db = new DataContext())
            {
                var join = from d1 in db.Users.Where(x => x.id == data.userid)
                           join d2 in db.employeesType on data.employeestypeid equals d2.id
                           join d3 in db.Hotel on data.hotelid equals d3.id
                           join d4 in db.Departman on d2.departmanid equals d4.Id
                           select new EmployeeResponse
                           {
                               EmployeeId = employeeId,
                               Name = data.Name,
                               Surname = data.Surname,
                               StartDateOfWork = data.StartDateOfWork,
                               ExitEntryDate = data.ExitEntryDate == null ? null : data.ExitEntryDate != null ? new DateTime(data.ExitEntryDate.Value.Year, data.ExitEntryDate.Value.Month, data.ExitEntryDate.Value.Day) : null,
                               phoneNumber = data.phoneNumber,
                               FloorNo = data.FloorNo,
                               gender = data.gender,
                               ImageUrl = data.ImageUrl,
                               hotelid = d3.id,
                               HotelName = d3.HotelName,
                               DepartmentId = d4.Id,
                               DepartmentName = d4.DepartmanName,
                               Employeestypeid = d2.id,
                               PositionName = d2.TypeName,
                               OnlineOrOfflineNow = data.OnlineOrOfflineNow,
                               LastOnlineTime = data.onlineTime == null ? null : data.onlineTime != null ? new DateTime(data.onlineTime.Value.Year, data.onlineTime.Value.Month, data.onlineTime.Value.Day) : null
                           };
                return join.FirstOrDefault();
            }


        }

        public UserAndDirectoryResponse GetDirectoryByDirectoryUserId(int directoryEmployeeId)
        {
            using (var db = new DataContext())
            {

                var joindata = from d1 in db.Employees.Where(x => x.isDeleted == false && x.isActive == true && x.id == directoryEmployeeId)
                               join d2 in db.Users.Where(x => x.isActive == true && x.isDeleted == false) on d1.userid equals d2.id
                               join d3 in db.Hotel.Where(x => x.isDeleted == false && x.isActive == true) on d1.hotelid equals d3.id
                               select new UserAndDirectoryResponse
                               {
                                   DirectoryEmployeeId = directoryEmployeeId,
                                   hotelid = d3.id,
                                   employeeid = d1.id,
                                   HotelName = d3.HotelName,
                                   name = d1.Name,
                                   surname = d1.Surname,
                                   email = d2.email,
                                   gender = d1.gender,
                                   imageurl = d1.ImageUrl,
                                   phonenumber = d1.phoneNumber,
                                   password = CommodMethods.ConvertDecrypt(d2.password),
                                   StartDateOfWork = new DateTime(d1.StartDateOfWork.Value.Year, d1.StartDateOfWork.Value.Month, d1.StartDateOfWork.Value.Day),
                                   ExitEntryDate = d1.ExitEntryDate != null ? new DateTime(d1.ExitEntryDate.Value.Year, d1.ExitEntryDate.Value.Month, d1.ExitEntryDate.Value.Day) : null,
                               };

                var datafirst = joindata.FirstOrDefault();

                var hotelEmployee = from d1 in joindata
                                    join d2 in db.DirectorsDepartmans.Where(x => x.isActive == true && x.isDeleted == false) on d1.DirectoryEmployeeId equals d2.directoryid
                                    join d3 in db.Departman on d2.departmanid equals d3.Id
                                    select new DirectoryDepartmentResponseAllDepartmentResponse { departmanid = d2.departmanid, directoryEmployeeId = d1.DirectoryEmployeeId, DepartmentName = d3.DepartmanName };

                datafirst.AllDepartment = hotelEmployee.ToList();

                return datafirst;
            }
        }

        public List<UserAndDirectoryResponse> GetDirectoryByHotelid(int hotelid)
        {
            using (var db = new DataContext())
            {
                var response = new List<UserAndDirectoryResponse>();
                var listjoin = from d1 in db.Hotel
                               join d2 in db.Employees.Where(x => x.isDeleted == false && x.isActive == true) on d1.id equals d2.hotelid
                               join d3 in db.Users.Where(x => x.roleid == 5 && x.isActive == true && x.isDeleted == false) on d2.userid equals d3.id
                               select new UserAndDirectoryResponse
                               {
                                   DirectoryEmployeeId = d2.id,
                                   name = d2.Name,
                                   surname = d2.Surname,
                                   email = d3.email,
                                   password = CommodMethods.ConvertDecrypt(d3.password),
                                   employeeid = d2.id,
                                   gender = d2.gender,
                                   hotelid = d1.id,
                                   HotelName = d1.HotelName,
                                   imageurl = d2.ImageUrl,
                                   phonenumber = d2.phoneNumber,
                                   StartDateOfWork = new DateTime(d2.StartDateOfWork.Value.Year, d2.StartDateOfWork.Value.Month, d2.StartDateOfWork.Value.Day),
                                   ExitEntryDate = d2.ExitEntryDate != null ? new DateTime(d2.ExitEntryDate.Value.Year, d2.ExitEntryDate.Value.Month, d2.ExitEntryDate.Value.Day) : null,
                               };
                var datalist = listjoin.ToList();

                var hotelemployee = from d1 in datalist
                                    join d2 in db.DirectorsDepartmans.Where(x => x.isActive == true && x.isDeleted == false) on d1.DirectoryEmployeeId equals d2.directoryid
                                    join d3 in db.Departman on d2.departmanid equals d3.Id
                                    select new DirectoryDepartmentResponseAllDepartmentResponse { departmanid = d2.departmanid, directoryEmployeeId = d1.DirectoryEmployeeId, DepartmentName = d3.DepartmanName };

                for (int i = 0; i < datalist.Count; i++)
                {
                    datalist[i].AllDepartment = hotelemployee.Where(x => x.directoryEmployeeId == datalist[i].DirectoryEmployeeId).ToList();
                    response.Add(datalist[i]);
                }
                return response;
            }
        }

        public List<UserAndDirectoryResponse> GetDirectoryByHotelidAndDepartmanId(int hotelid, int departmanid)
        {
            using (var db = new DataContext())
            {
                var response = new List<UserAndDirectoryResponse>();
                var joinList = from d1 in db.Hotel.Where(x => x.id == hotelid && x.isActive == true && x.isDeleted == false)
                               join d2 in db.Employees on d1.id equals d2.hotelid
                               join d3 in db.Users.Where(x => x.roleid == 5 && x.isActive == true && x.isDeleted == false) on d2.userid equals d3.id
                               select new UserAndDirectoryResponse
                               {
                                   DirectoryEmployeeId = d2.id,
                                   name = d2.Name,
                                   surname = d2.Surname,
                                   email = d3.email,
                                   password = CommodMethods.ConvertDecrypt(d3.password),
                                   employeeid = d2.id,
                                   HotelName = d1.HotelName,
                                   gender = d2.gender,
                                   hotelid = d1.id,
                                   imageurl = d2.ImageUrl,
                                   ExitEntryDate = d2.ExitEntryDate != null ? new DateTime(d2.ExitEntryDate.Value.Year, d2.ExitEntryDate.Value.Month, d2.ExitEntryDate.Value.Day) : null,
                                   StartDateOfWork = d2.StartDateOfWork,
                                   phonenumber = d2.phoneNumber
                               };
                var listData = joinList.ToList();

                var hotelemployee = from d1 in joinList
                                    join d2 in db.DirectorsDepartmans.Where(x => x.isActive == true && x.isDeleted == false && x.departmanid == departmanid) on d1.DirectoryEmployeeId equals d2.directoryid
                                    join d3 in db.Departman on d2.departmanid equals d3.Id
                                    select new DirectoryDepartmentResponseAllDepartmentResponse { departmanid = d2.departmanid, directoryEmployeeId = d1.DirectoryEmployeeId, DepartmentName = d3.DepartmanName };

                for (int i = 0; i < listData.Count; i++)
                {
                    listData[i].AllDepartment = hotelemployee.Where(x => x.directoryEmployeeId == listData[i].DirectoryEmployeeId).ToList();
                    if (listData[i].AllDepartment.Count > 0)
                        response.Add(listData[i]);
                }
                return response;
            }
        }

        public List<EmployeeResponse> GetEmployeeByEmployeeTypeId(int employeeTypeId, int hotelid)
        {
            using (var db = new DataContext())
            {
                var query = from d1 in db.Employees
                            join d2 in db.Users on d1.userid equals d2.id
                            join d3 in db.Companies on d2.companyid equals d3.id
                            join d4 in db.Hotel on d1.hotelid equals d4.id into htl
                            from hoteljoin in htl.DefaultIfEmpty()
                            join d5 in db.employeesType on d1.employeestypeid equals d5.id
                            join d6 in db.Departman on d5.departmanid equals d6.Id into dpr
                            from departmentjoin in dpr.DefaultIfEmpty()
                            where d1.isActive == true && d1.isDeleted == false && d2.isActive == true && d2.isDeleted == false
                            && d3.isActive == true && d3.isDeleted == false && hoteljoin.isDeleted == false && hoteljoin.isActive == true
                            && departmentjoin.isActive == true && departmentjoin.isDeleted == false && d5.isDeleted == false && d5.isActive == true
                            select new EmployeeResponse
                            {
                                EmployeeId = d1.id,
                                StartDateOfWork = d1.StartDateOfWork,
                                DepartmentId = departmentjoin.Id,
                                DepartmentName = departmentjoin.DepartmanName,
                                ExitEntryDate = d1.ExitEntryDate,
                                Employeestypeid = (int)d1.employeestypeid,
                                FloorNo = d1.FloorNo,
                                gender = d1.OnlineOrOfflineNow,
                                hotelid = d1.hotelid,
                                HotelName = hoteljoin.HotelName,
                                Surname = d1.Surname,
                                ImageUrl = d1.ImageUrl,
                                LastOnlineTime = d1.onlineTime,
                                Name = d1.Name,
                                OnlineOrOfflineNow = d1.OnlineOrOfflineNow,
                                phoneNumber = d1.phoneNumber,
                                PositionName = d5.TypeName,
                                userid = d1.userid
                            };
                return query.ToList();
            }
        }

        public ManagementUserResponse GetManagementById(int managerUserId)
        {
            using (var db = new DataContext())
            {
                var joinFirst = from d1 in db.Users.Where(x => x.id == managerUserId && x.isActive == true && x.isDeleted == false)
                                join d2 in db.Employees.Where(x => x.isDeleted == false && x.isActive == true) on d1.id equals d2.userid
                                join d3 in db.Companies.Where(x => x.isActive == true && x.isDeleted == false) on d1.companyid equals d3.id
                                select new ManagementUserResponse
                                {
                                    Id = d1.id,
                                    CompanyName = d3.name,
                                    surname = d2.Surname,
                                    name = d2.Name,
                                    email = d1.email,
                                    password = CommodMethods.ConvertDecrypt(d1.password),
                                    StartDateOfWork = d2.StartDateOfWork,
                                    EmployeeTypeName = "Management",
                                    ExitEntryDate = d2.ExitEntryDate,
                                    gender = d2.gender,
                                    imageurl = d2.ImageUrl,
                                    phonenumber = d2.phoneNumber,
                                    employeestypeid = d2.employeestypeid,
                                    companyId = d3.id
                                };


                var data = joinFirst.FirstOrDefault();
                return data;
            }
        }

        public Employees GetEmployeeByUserId(int userId)
        {
            using (var db = new DataContext())
            {
                var data = db.Employees.Where(x => x.userid == userId).FirstOrDefault();
                if (data == null)
                    throw new ClientSideException($"{typeof(Employees).Name}  Not Found");
                return data;
            }
        }
    }
}
