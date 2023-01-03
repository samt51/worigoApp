using System.Collections.Generic;
using Worigo.Core.Dtos.Departman.Request;
using Worigo.Core.Dtos.Departman.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;

namespace Worigo.Business.Abstrack
{
    public interface IDepartmanService
    {
        ResponseDto<DepartmentCommentRateResponse> DepartmanCommentRateResponse(int hotelid, int departmanid, TokenKeys keys);
        ResponseDto<List<DepartmentResponse>> GetAll(TokenKeys keys);
        ResponseDto<DepartmentResponse> GetById(int id, TokenKeys keys);
        ResponseDto<DepartmentResponse> Create(DepartmentAddOrUpdateRequest entity, TokenKeys keys);
        ResponseDto<DepartmentResponse> Update(DepartmentAddOrUpdateRequest entity, TokenKeys keys);
    }
}
