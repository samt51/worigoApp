using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Enum;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class GeneralServiceController : CustomBaseController
    {
        private readonly IGeneralServiceService _generalServiceService;
        private readonly IMapper _mapper;
        public GeneralServiceController(IGeneralServiceService generalServiceService, IMapper mapper)
        {
            _generalServiceService = generalServiceService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var generalServiceListData = _generalServiceService.GetAll();
            var generalServiceListDto = _mapper.Map<List<GeneralServiceDto>>(generalServiceListData);
            return CreateActionResult(ResponseDto<List<GeneralServiceDto>>.Success(generalServiceListDto, 200));
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var generalServiceSingularData = _generalServiceService.GetById(id);
            var generalServiceSingularDto = _mapper.Map<GeneralServiceDto>(generalServiceSingularData);
            return CreateActionResult(ResponseDto<GeneralServiceDto>.Success(generalServiceSingularDto, 200));
        }
        [HttpPost]
        public IActionResult Add(GeneralServiceDto entity)
        {
            var entitydto = new GeneralService
            {
                CreatedDate = System.DateTime.Now,
                ModifyDate = System.DateTime.Now,
                isDeleted = false,
                name = entity.name,
                ImageUrl = entity.ImageUrl,
            };
            _generalServiceService.Create(_mapper.Map<GeneralService>(entitydto));
            return CreateActionResult(ResponseDto<GeneralService>.Success(200));
        }
        [HttpPost("{id}")]
        public IActionResult Delete(int id)
        {
            var generalServiceSingularData = _generalServiceService.GetById(id);
            generalServiceSingularData.isDeleted = true;
            _generalServiceService.Update(generalServiceSingularData);
            return CreateActionResult(ResponseDto<GeneralService>.Success(200));
        }
        [HttpPost]
        public IActionResult Update(GeneralServiceDto entity)
        {
            var generalServiceSingularData = _generalServiceService.GetById(entity.id);
            generalServiceSingularData.ModifyDate = System.DateTime.Now;
            generalServiceSingularData.ImageUrl = entity.ImageUrl;
            generalServiceSingularData.name = entity.name;
            _generalServiceService.Update(generalServiceSingularData);
            return CreateActionResult(ResponseDto<GeneralService>.Success(200));
        }
    }
}
