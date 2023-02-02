using AutoMapper;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.Companies.Request;
using Worigo.Core.Dtos.Companies.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class CompaniesManager : ICompaniesService
    {
        private readonly ICompaniesDal _companiesDal;
        private readonly IMapper _mapper;
        public CompaniesManager(ICompaniesDal companiesDal, IMapper mapper)
        {
            _companiesDal = companiesDal;
            _mapper = mapper;
        }

        public ResponseDto<CompaniesResponse> Create(CompaniesAddOrUpdateRequest entity, TokenKeys keys)
        {
            if (keys.role == 1)
            {
                var data = _mapper.Map<Companies>(entity);
                var response = _mapper.Map<CompaniesResponse>(_companiesDal.Create(data));
                return new ResponseDto<CompaniesResponse>().Success(response, 200);
            }
            return new ResponseDto<CompaniesResponse>().Authorization();
        }

        public ResponseDto<List<CompaniesResponse>> GetAll(TokenKeys keys)
        {
            if (keys.role == 1)
            {
                var data = _companiesDal.GetAll(x => x.isActive == true && x.isDeleted == false);
                var response = _mapper.Map<List<CompaniesResponse>>(data);
                return new ResponseDto<List<CompaniesResponse>>().Success(response, 200);
            }
            return new ResponseDto<List<CompaniesResponse>>().Authorization();
        }

        public ResponseDto<CompaniesResponse> GetById(int id, TokenKeys keys)
        {
            var companies = _companiesDal.GetById(id);
            if (keys.role == 2 && companies.id == keys.companyid || keys.role == 1)
            {
                var data = _companiesDal.GetById(id);
                var response = _mapper.Map<CompaniesResponse>(data);
                return new ResponseDto<CompaniesResponse>().Success(response, 200);
            }
            return new ResponseDto<CompaniesResponse>().Authorization();
        }

        public ResponseDto<CompaniesResponse> Update(CompaniesAddOrUpdateRequest entity, TokenKeys keys)
        {
            var companies = _companiesDal.GetById(entity.id);
            if (keys.role == 2 && keys.companyid == companies.id || keys.role == 1)
            {
                var data = _mapper.Map<Companies>(entity);
                var update = _companiesDal.Update(data);
                var response = _mapper.Map<CompaniesResponse>(update);
                return new ResponseDto<CompaniesResponse>().Success(response, 200);
            }
            return new ResponseDto<CompaniesResponse>().Authorization();
        }
    }
}
