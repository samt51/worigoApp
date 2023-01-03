using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.API.Model.UserViewModel;
using Worigo.Business.Abstrack;
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
        private readonly IHotelService _hotelService;
        private readonly IMapper _mapper;
        public CompaniesController(ICompaniesService companiesService, IMapper mapper, IHotelService hotelService)
        {
            _companiesService = companiesService;
            _mapper = mapper;
            _hotelService = hotelService;   
        }
        [HttpGet]
        public IActionResult GetAll([FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys.role == 1)
            {
                var listcompanies = _companiesService.GetAll();
                var listcompaniesDto = _mapper.Map<List<CompaniesDto>>(listcompanies);
                return CreateActionResult(ResponseDto<List<CompaniesDto>>.Success(listcompaniesDto, 200));
            }
            return CreateActionResult(ResponseDto<List<Companies>>.Authorization());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id, [FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var listcompanies = _companiesService.GetById(id);
            var listcompaniesDto = _mapper.Map<CompaniesDto>(listcompanies);
            if (keys.role == 2 ||  keys.role == 1)
            {
                return CreateActionResult(ResponseDto<CompaniesDto>.Success(listcompaniesDto, 200));
            }
            return CreateActionResult(ResponseDto<List<Companies>>.Authorization());
        }
        [HttpPost]
        public IActionResult Add(CompaniesDto companies, [FromHeader] string Authorization)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys.role == 1)
            {
                var entity = new Companies
                {
                    CreatedDate = System.DateTime.Now,
                    ModifyDate = System.DateTime.Now,
                    isActive = true,
                    isDeleted = false,
                    name = companies.name,
                };
                _companiesService.Create(entity);
                return CreateActionResult(ResponseDto<Companies>.Success(200));
            }
            return CreateActionResult(ResponseDto<List<Companies>>.Authorization());
        }
        [HttpPost]
        public IActionResult Update(CompaniesDto companies, [FromHeader] string Authorization)
        {
            var companiesdata = _companiesService.GetById(companies.id);
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys.role == 1)
            {
                companiesdata.name = companies.name;
                _companiesService.Update(_mapper.Map<Companies>( companies));
                return CreateActionResult(ResponseDto<Companies>.Success(200));
            }
            return CreateActionResult(ResponseDto<List<Companies>>.Authorization());
        }
        
    }
}