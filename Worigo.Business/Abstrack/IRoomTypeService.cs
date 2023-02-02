using System.Collections.Generic;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.RoomType.Request;
using Worigo.Core.Dtos.RoomType.Response;

namespace Worigo.Business.Abstrack
{
    public interface IRoomTypeService
    {
        ResponseDto<List<RoomTypeResponse>> GetAll(TokenKeys keys);
        ResponseDto<RoomTypeResponse> GetById(int id, TokenKeys keys);
        ResponseDto<RoomTypeResponse> Create(RoomTypeAddOrUpdateRequest entity, TokenKeys keys);
        ResponseDto<RoomTypeResponse> Update(RoomTypeAddOrUpdateRequest entity, TokenKeys keys);
    }
}
