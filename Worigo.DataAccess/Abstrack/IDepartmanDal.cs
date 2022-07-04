using System.Collections.Generic;
using Worigo.Core.Dtos.JoinClass;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface IDepartmanDal:IRepositoryDesignPattern<Departman>
    {
        List<DepartmanAndHotelJoin> GetAllJoin(int hotelid);
    }
}
