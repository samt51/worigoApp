using System.Collections.Generic;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.ServicesValue.Request;
using Worigo.Core.Dtos.ServicesValue.Response;

namespace Worigo.Business.Abstrack
{
    public interface IServiceValueService
    {
        ResponseDto<List<ServicesValueResponse>> GetValueByServiceId(int serviceid, TokenKeys keys);
        ResponseDto<ServicesValueResponse> GetById(int id, TokenKeys keys);
        ResponseDto<ServicesValueResponse> Create(ServicesValuesAddOrUpdateRequest request, TokenKeys keys);
        ResponseDto<ServicesValueResponse> Update(ServicesValuesAddOrUpdateRequest request, TokenKeys keys);
    }
}
