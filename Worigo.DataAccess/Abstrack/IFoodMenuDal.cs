using System.Collections.Generic;
using Worigo.Core.Dtos.FoodMenu.Response;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface IFoodMenuDal: IRepositoryDesignPattern<FoodMenu>
    {
        List<NewMenuListResponse> GetMenuByHotelId(int hotelId);    
    }
}
