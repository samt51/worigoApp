using Worigo.Core.Dtos.Departman.Response;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface IDepartmanDal:IRepositoryDesignPattern<Departman>
    {
        DepartmentCommentRateResponse DepartmanCommentRateResponse(int hotelid, int departmanid);
    }
}
