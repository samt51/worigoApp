using System.Collections.Generic;
using Worigo.Core.Dtos.DirectorsDepartmans.Response;
using Worigo.Core.Dtos.Employee.Response;
using Worigo.Core.Dtos.ManagerDto.Response;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface IEmployeesDal: IRepositoryDesignPattern<Employees>
    {
        List<EmployeeResponse> GetEmployeesByHotelId(int hotelid);
        EmployeeResponse GetEmployeeByEmployeeId(int employeeId);
        List<EmployeeResponse> GetEmployeeByEmployeeTypeId(int employeeTypeId,int hotelid);
        List<UserAndDirectoryResponse> GetDirectoryByHotelid(int hotelid);
        List<UserAndDirectoryResponse> GetDirectoryByHotelidAndDepartmanId(int hotelid,int departmanid);
        ManagementResponse GetManagementById(int managerUserId);
        UserAndDirectoryResponse GetDirectoryByDirectoryUserId(int directoryEmployeeId);
        Employees GetEmployeeByUserId(int userId);
    }
}
