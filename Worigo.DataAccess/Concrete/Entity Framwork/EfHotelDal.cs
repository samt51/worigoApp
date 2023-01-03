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
                var joinlist = from d1 in db.Hotel
                               join d2 in db.Companies on d1.Companyid equals d2.id
                               where d1.isActive == true && d1.isDeleted == false && d2.isActive == true && d2.isDeleted == false
                               select new HotelResponse
                               {
                                   NumberOfStar = d1.NumberOfStar,
                                   Adress = d1.Adress,
                                   CompanyName = d2.name,
                                   Email = d1.Email,
                                   HotelName = d1.HotelName,
                                   id = d1.id,
                                   ImageUrl = d1.ImageUrl,
                                   PhoneNumber = d1.PhoneNumber,
                                   Companyid = d2.id
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
