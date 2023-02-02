using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.API.Model.UserViewModel;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.Companies.Request;
using Worigo.Core.Dtos.Companies.Response;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CompaniesController : CustomBaseController
    {
        private readonly ICompaniesService _companiesService;

        public CompaniesController(ICompaniesService companiesService)
        {
            _companiesService = companiesService;
        }
        [HttpGet]
        public ResponseDto<List<CompaniesResponse>> GetAll([FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _companiesService.GetAll(keys);
        }
        [HttpGet("{id}")]
        public ResponseDto<CompaniesResponse> GetById(int id, [FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _companiesService.GetById(id, keys);
        }
        [HttpPost]
        public ResponseDto<CompaniesResponse> Add(CompaniesAddOrUpdateRequest companies, [FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _companiesService.Create(companies, keys);
        }
        [HttpPost]
        public ResponseDto<CompaniesResponse> Update(CompaniesAddOrUpdateRequest companies, [FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _companiesService.Update(companies, keys);
        }

    }
}