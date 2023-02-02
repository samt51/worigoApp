using System.Collections.Generic;
using Worigo.Core.Dtos.Companies.Request;
using Worigo.Core.Dtos.Companies.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;

namespace Worigo.Business.Abstrack
{
    public interface ICompaniesService
    {
        ResponseDto<List<CompaniesResponse>> GetAll(TokenKeys keys);
        ResponseDto<CompaniesResponse> GetById(int id, TokenKeys keys);
        ResponseDto<CompaniesResponse> Create(CompaniesAddOrUpdateRequest entity, TokenKeys keys);
        ResponseDto<CompaniesResponse> Update(CompaniesAddOrUpdateRequest entity, TokenKeys keys);
    }
}
