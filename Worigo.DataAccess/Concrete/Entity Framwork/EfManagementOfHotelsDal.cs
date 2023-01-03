using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Exceptions;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfManagementOfHotelsDal : EfRepositoryDal<ManagementOfHotels, DataContext>, IManagementOfHotelsDal
    {
        public ManagementOfHotels GetManagementBymanagementIdByHotelid(int managementid, int hotelid)
        {
            using (var db = new DataContext())
            {
                var entity = db.ManagementOfHotels.Where(x => x.hotelid == hotelid && x.managementid == managementid).FirstOrDefault();
                if (entity == null)
                {
                    throw new ClientSideException($"{typeof(ManagementOfHotels).Name}  Not Found");
                }
                return entity;
            }
        }

        public List<HotelDto> GetManagementHotel(int managementid)
        {
            using (var db = new DataContext())
            {
                var hotellistdto = new List<HotelDto>();
                var hotelid = db.ManagementOfHotels.Where(x => x.managementid == managementid).ToList();
                foreach (var item in hotelid)
                {
                    var joinlist = db.Hotel.Where(x => x.id == item.hotelid).FirstOrDefault();
                    var entity = new HotelDto
                    {
                        id = joinlist.id,
                        NumberOfStar = joinlist.NumberOfStar,
                        Adress = joinlist.Adress,
                        Email = joinlist.Email,
                        HotelName = joinlist.HotelName,
                        ImageUrl = joinlist.ImageUrl,
                        PhoneNumber = joinlist.PhoneNumber
                    };

                    hotellistdto.Add(entity);
                }

                return hotellistdto;
            }
        }

        public void IsthereManagementByDepartmentDirectory(int managementId, int directoryEmployeeId)
        {
            using (var db=new DataContext())
            {
                var management = db.ManagementOfHotels.Where(x => x.managementid == managementId).ToList();
                var directory = db.DirectorsDepartmans.Where(x => x.directoryid == directoryEmployeeId).ToList();
                var directoryHotelId = directory.GroupBy(x => x.hotelid).ToList();
                directoryHotelId.ForEach(x =>
                {
                    if(management.Where(x => x.hotelid == x.hotelid).First()==null)
                        throw new NotFoundException("Data Not Found");
                });
            }
        }
    }
}
