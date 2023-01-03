using System.Collections.Generic;
using Worigo.Core.Dtos.FoodMenuDetailDto.Response;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface IFoodMenuDetailDal: IRepositoryDesignPattern<FoodMenuDetail>
    {
        List<FoodMenuDetailResponse> GetAllByMenuId(int menuid);
        FoodMenuDetailResponse GetByDetailId(int menuDetailid); 
    }
}
