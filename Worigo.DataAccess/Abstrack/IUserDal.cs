using System.Collections.Generic;
using Worigo.Core.Dtos.Employee.Request;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.ManagerDto.Response;
using Worigo.Core.Dtos.User.Response;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface IUserDal:IRepositoryDesignPattern<User>
    {
        User GetUserByEmailAndPassword(string email, string password);
        User GetUserByEmail(string email);  
        User LastAddUser();
        List<AddHotelAdminModelDto> GetHotelAdminByCompaniesId(int companiesid);
        AddHotelAdminModelDto GetHotelAdminByAdminUserId(int adminUserId);
        List<ManagementResponse> GetManagemetByCompaniesid(int companiesid);
        List<UserResponse> GetAllJoin();
        List<UserResponse> GetUserByHotelid(int hotelid);
        List<ManagementResponse> GetManagemetByHotelid(int hotelid);
        List<EmployeesAndUserListDto> GetEmployeesByHotelid(int hotelid, int roleid);
        UserResponse UserGetByIdJoin(int id);
    }
}
