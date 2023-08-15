using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.ServicesValue.Response;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfServicesValueDal : EfRepositoryDal<ServicesValues, DataContext>, IServicesValuesDal
    {
        public List<ServicesValueResponse> GetValueByServiceId(int serviceid, int hotelId)
        {
            using (var db = new DataContext())
            {
                var joinlist = from d1 in db.ServicesValues.Where(x => x.Serviceid == serviceid)
                               join d2 in db.Services on d1.Serviceid equals d2.id
                               select new ServicesValueResponse
                               {
                                   id = d1.id,
                                   value = d1.value,
                                   ServiceName = d2.Name,
                                   Serviceid = serviceid,
                                   ImageUrl = d1.ImageUrl

                               };
                return joinlist.ToList();
            }
        }
    }
}
