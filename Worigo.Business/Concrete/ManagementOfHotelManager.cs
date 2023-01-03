using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.ListDto;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class ManagementOfHotelManager : IManagementOfHotelService
    {
        private readonly IManagementOfHotelsDal _managementOfHotelsDal;
        public ManagementOfHotelManager(IManagementOfHotelsDal managementOfHotelsDal)
        {
            _managementOfHotelsDal = managementOfHotelsDal;
        }
        public ManagementOfHotels Create(ManagementOfHotels entity)
        {
          return  _managementOfHotelsDal.Create(entity);
        }

        public ManagementOfHotels Delete(ManagementOfHotels entity)
        {
          return  _managementOfHotelsDal.Delete(entity);
        }

        public List<ManagementOfHotels> GetAll()
        {
            return _managementOfHotelsDal.GetAll();
        }

        public ManagementOfHotels GetById(int id)
        {
            return _managementOfHotelsDal.GetById(id);
        }

        public ManagementOfHotels GetManagementBymanagementIdByHotelid(int managementid, int hotelid)
        {
           return _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(managementid, hotelid);   
        }

        public List<HotelDto> GetManagementHotel(int managementid)
        {
            return _managementOfHotelsDal.GetManagementHotel(managementid);
        }

        public void IsthereManagementByDepartmentDirectory(int managementId, int directoryEmployeeId)
        {
             _managementOfHotelsDal.IsthereManagementByDepartmentDirectory(managementId, directoryEmployeeId);
        }

        public ManagementOfHotels Update(ManagementOfHotels entity)
        {
          return  _managementOfHotelsDal.Update(entity);
        }
    }
}
