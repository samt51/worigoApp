using Worigo.Core.Dtos.DirectorsDepartmans.Request;
using Worigo.Core.Dtos.DirectorsDepartmans.Response;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface IDirectorsDepartmansDal: IRepositoryDesignPattern<DirectorsDepartmans>
    {
        DirectorsDepartmans GetDirectoryByHotelIdAndId(int hotelid, int id);
        UserAndDirectoryResponse GetDirectoryByUserId(int directoryUserId);
        void ToDepartmentManagerRemove(UserAndDirectoryDepartmentAddOrUpdateRequest request);
    }
}
