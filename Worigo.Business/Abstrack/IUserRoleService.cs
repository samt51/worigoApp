using System.Collections.Generic;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.UserRole.Request;
using Worigo.Core.Dtos.UserRole.Response;

namespace Worigo.Business.Abstrack
{
    public interface IUserRoleService
    {
        ResponseDto<List<UserRoleResponse>> GetAll(TokenKeys keys);
        ResponseDto<UserRoleResponse> GetById(int id, TokenKeys keys);
        ResponseDto<UserRoleResponse> Create(UserRoleRequest entity, TokenKeys keys);
        ResponseDto<UserRoleResponse> Update(UserRoleRequest entity, TokenKeys keys);
    }
}
