using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.Hotel.Response;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfHotelDal : EfRepositoryDal<Hotel, DataContext>, IHotelDal
    {
        public List<HotelResponse> GetHotelByCompanyid(int companyid)
        {
            using (var db = new DataContext())
            {
                var joinlist = from d1 in db.Companies.Where(x => x.id == companyid)
                               join d2 in db.Hotel on d1.id equals d2.Companyid
                               where d1.isActive == true && d1.isDeleted == false && d2.isActive == true && d2.isDeleted == false
                               select new HotelResponse
                               {
                                   NumberOfStar = d2.NumberOfStar,
                                   Adress = d2.Adress,
                                   CompanyName = d1.name,
                                   Email = d2.Email,
                                   HotelName = d2.HotelName,
                                   id = d2.id,
                                   ImageUrl = d2.ImageUrl,
                                   PhoneNumber = d2.PhoneNumber,
                                   Companyid = d1.id
                               };
                return joinlist.ToList();
            }
        }

        public HotelResponse GetHotelByCompanyIdAndHotelId(int companyid, int hotelid)
        {
            using (var db = new DataContext())
            {
                var join = from d1 in db.Hotel
                           join d2 in db.Companies on d1.Companyid equals d2.id
                           where d1.isActive == true && d1.isDeleted == false && d2.isDeleted == false && d2.isActive == true
                           select new HotelResponse
                           {
                               Adress = d1.Adress,
                               NumberOfStar = d1.NumberOfStar,
                               Companyid = d1.Companyid,
                               CompanyName = d2.name,
                               Email = d1.Email,
                               HotelName = d1.HotelName,
                               ImageUrl = d1.ImageUrl,
                               PhoneNumber = d1.PhoneNumber,
                               id = d1.id
                           };
                var data = join.FirstOrDefault();
                if (data == null)
                {
                    throw new System.Exception("Data Not Found");
                }
                return data;

            }
        }








    }
}
