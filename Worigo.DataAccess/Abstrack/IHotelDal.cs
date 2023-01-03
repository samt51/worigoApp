using System.Collections.Generic;
using Worigo.Core.Dtos.Hotel.Response;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface IHotelDal:IRepositoryDesignPattern<Hotel>
    {
        List<HotelResponse> GetHotelByCompanyid(int companyid);
        HotelResponse GetHotelByCompanyIdAndHotelId(int companyid, int hotelid);
    }
}
