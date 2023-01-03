using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.FoodMenuDetailDto.Dto;
using Worigo.Core.Dtos.FoodMenuDetailDto.Response;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class FoodMenuDetailManager : IFoodMenuDetailService
    {
        private readonly IFoodMenuDetailDal _foodMenuDetailDal;
        private readonly IContentsOfFoodDal _contentsOfFoodDal;
        public FoodMenuDetailManager(IFoodMenuDetailDal foodMenuDetailDal, IContentsOfFoodDal contentsOfFoodDal)
        {
            _foodMenuDetailDal = foodMenuDetailDal;
            _contentsOfFoodDal = contentsOfFoodDal; 
        }
        public FoodMenuDetail Create(FoodMenuDetailDtoAddOrUpdateRequest entity)
        {
           return _foodMenuDetailDal.Create(null);
        }

        public List<FoodMenuDetailResponse> GetAllByMenuId(int menuid)
        {
            return _foodMenuDetailDal.GetAllByMenuId(menuid);
        }

        public FoodMenuDetailResponse GetByDetailId(int menuDetailid)
        {
            return _foodMenuDetailDal.GetByDetailId(menuDetailid);
        }

        public FoodMenuDetail Update(FoodMenuDetailDtoAddOrUpdateRequest entity)
        {
          return  _foodMenuDetailDal.Update(null);
        }
    }
}
