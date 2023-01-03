using System.Collections.Generic;
using Worigo.Core.Dtos.FoodMenu.Response;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IFoodMenuService
    {
        List<NewMenuListResponse> GetMenuByHotelId(int hotelId);
        FoodMenu GetById(int id);
        FoodMenu Create(FoodMenu entity);
        FoodMenu Update(FoodMenu entity);
    }
}
