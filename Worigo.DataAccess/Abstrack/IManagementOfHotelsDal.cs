using System.Collections.Generic;
using Worigo.Core.Dtos.ListDto;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface IManagementOfHotelsDal : IRepositoryDesignPattern<ManagementOfHotels>
    {
        ManagementOfHotels GetManagementBymanagementIdByHotelid(int managementid, int hotelid);
        List<HotelDto> GetManagementHotel(int managementid);
        void IsthereManagementByDepartmentDirectory(int managementId, int directoryEmployeeId);
        void AuthorizeControll(int role, int userid,int hotelId,int companyId);
    }
}
