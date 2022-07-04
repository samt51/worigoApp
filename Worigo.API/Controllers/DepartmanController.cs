using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Enum;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DepartmanController : CustomBaseController
    {
        private readonly IDepartmanService _departmanService;
        private readonly IMapper _mapper;
        public DepartmanController(IDepartmanService departmanService, IMapper mapper)
        {
            _departmanService = departmanService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll(int hotelid)
        {
            var departmanList = _departmanService.GetAllJoin(hotelid);
            return CreateActionResult(ResponseDto<List<DepartmanAndHotelJoin>>.Success(departmanList, 200));
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var departmanSingularData = _departmanService.GetById(id);
            var departmanSingularDto = _mapper.Map<DepartmanDto>(departmanSingularData);
            return CreateActionResult(ResponseDto<DepartmanDto>.Success(departmanSingularDto,200));
        }
        [HttpPost]
        public IActionResult Add(DepartmanDto entity)
        {
            var entitydto = new Departman
            {
                CreatedDate = System.DateTime.Now,
                ModifyDate = System.DateTime.Now,
                isDeleted = false,
                DepartmanName = entity.DepartmanName,
                isActive = entity.isActive,
                Hotelid=entity.Hotelid,
            };
            _departmanService.Create(_mapper.Map<Departman>(entitydto));
            return CreateActionResult(ResponseDto<Departman>.Success(200));
        }
        [HttpPost("{id}")]
        public IActionResult Delete(int id)
        {
            var departmanSingularData = _departmanService.GetById(id);
            departmanSingularData.isDeleted = true;
            _departmanService.Update(departmanSingularData);
            return CreateActionResult(ResponseDto<Departman>.Success(200));
        }
        [HttpPost]
        public IActionResult Update(DepartmanDto entity)
        {
            var departmanSingularData = _departmanService.GetById(entity.Id);
            departmanSingularData.ModifyDate = System.DateTime.Now;
            departmanSingularData.Hotelid = entity.Hotelid;
            departmanSingularData.DepartmanName = entity.DepartmanName;
            _departmanService.Update(departmanSingularData);
            return CreateActionResult(ResponseDto<Departman>.Success(200));
        }
    }
}
