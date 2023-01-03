using System.Collections.Generic;
using Worigo.Core.Dtos.ListDto;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IManagementOfHotelService
    {
        ManagementOfHotels GetManagementBymanagementIdByHotelid(int managementid, int hotelid);
        List<HotelDto> GetManagementHotel(int managementid);
        List<ManagementOfHotels> GetAll();
        ManagementOfHotels GetById(int id);
        ManagementOfHotels Create(ManagementOfHotels entity);
        ManagementOfHotels Update(ManagementOfHotels entity);
        ManagementOfHotels Delete(ManagementOfHotels entity);
        void IsthereManagementByDepartmentDirectory(int managementId, int directoryEmployeeId);
    }
}
