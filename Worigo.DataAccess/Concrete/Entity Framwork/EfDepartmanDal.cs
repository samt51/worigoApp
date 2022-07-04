using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.JoinClass;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfDepartmanDal : EfRepositoryDal<Departman, DataContext>, IDepartmanDal
    {
        public List<DepartmanAndHotelJoin> GetAllJoin(int hotelid)
        {
            using (var db = new DataContext())
            {
                var joinlist = from d1 in db.Departman.Where(x => x.isDeleted == false&&x.Hotelid==hotelid)
                               join d2 in db.Hotel on d1.Hotelid equals d2.id
                               select new DepartmanAndHotelJoin
                               {
                                   DepartmanName = d1.DepartmanName,
                                   Id = d1.Id,
                                   Hotel = d2.HotelName
                               };
                return joinlist.ToList();
            }
        }
    }
}
