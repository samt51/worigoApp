using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.JoinClass;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfServiceDal : EfRepositoryDal<Services, DataContext>, IServicesDal
    {
        public List<ServiceAndHotelJoin> serviceAndHotelJoins(int hoteid)
        {
            using (var db=new DataContext())
            {
                var listJoin = from d1 in db.Services.Where(x => x.hotelid == hoteid && x.isDeleted == false)
                               join d2 in db.Hotel on d1.hotelid equals d2.id
                               select new ServiceAndHotelJoin
                               {
                                   hotel=d2.HotelName,
                                   Name=d1.Name,
                                   id=d1.id
                               };
                return listJoin.ToList();
            }
        }
    }
}
