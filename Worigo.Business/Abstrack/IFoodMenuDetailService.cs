using System.Collections.Generic;
using Worigo.Core.Dtos.FoodMenuDetailDto.Dto;
using Worigo.Core.Dtos.FoodMenuDetailDto.Response;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IFoodMenuDetailService
    {
        List<FoodMenuDetailResponse> GetAllByMenuId(int menuid);
        FoodMenuDetailResponse GetByDetailId(int menuDetailid);
        FoodMenuDetail Create(FoodMenuDetailDtoAddOrUpdateRequest entity);
        FoodMenuDetail Update(FoodMenuDetailDtoAddOrUpdateRequest entity);
 
    }
}
