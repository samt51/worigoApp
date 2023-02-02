using System.Collections.Generic;
using Worigo.Core.Dtos.FoodMenuDetailDto.Dto;
using Worigo.Core.Dtos.FoodMenuDetailDto.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;

namespace Worigo.Business.Abstrack
{
    public interface IFoodMenuDetailService
    {
        ResponseDto<List<FoodMenuDetailResponse>> GetAllByMenuId(int menuid, TokenKeys keys);
        ResponseDto<FoodMenuDetailResponse> GetByDetailId(int menuDetailid, TokenKeys keys);
        ResponseDto<FoodMenuDetailResponse> Create(FoodMenuDetailDtoAddOrUpdateRequest entity, TokenKeys keys);
        ResponseDto<FoodMenuDetailResponse> Update(FoodMenuDetailDtoAddOrUpdateRequest entity, TokenKeys keys);

    }
}
