using System.Collections.Generic;
using Worigo.Core.Dtos.FoodMenu.Request;
using Worigo.Core.Dtos.FoodMenu.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;

namespace Worigo.Business.Abstrack
{
    public interface IFoodMenuService
    {
        ResponseDto<List<FoodMenuResponse>> GetMenuByHotelId(int hotelId, TokenKeys keys);
        ResponseDto<FoodMenuResponse> GetById(int id, TokenKeys keys);
        ResponseDto<FoodMenuResponse> Create(FoodMenuRequest entity, TokenKeys keys);
        ResponseDto<FoodMenuResponse> Update(FoodMenuRequest entity, TokenKeys keys);
    }
}
