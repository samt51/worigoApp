using Microsoft.AspNetCore.Mvc;
using Worigo.Core.Dtos.DirectorsDepartmans.Request;
using Worigo.Core.Dtos.DirectorsDepartmans.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;

namespace Worigo.Business.Abstrack
{
    public interface IDirectorsDepartmansService
    {
        ResponseDto<UserAndDirectoryResponse> GetDirectoryByHotelIdAndId(int hotelid, int id);
        ResponseDto<UserAndDirectoryResponse> Create(UserAndDirectoryDepartmentAddOrUpdateRequest entity);
        ResponseDto<UserAndDirectoryResponse> Update(UserAndDirectoryDepartmentAddOrUpdateRequest entity);
        ResponseDto<NoContentResult> ToDepartmentManagerRemove(TokenKeys keys,UserAndDirectoryDepartmentAddOrUpdateRequest request);
    }
}
