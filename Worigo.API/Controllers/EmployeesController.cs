using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class EmployeesController : CustomBaseController
    {
        private readonly IEmployeesService _employeesService;
        private readonly IMapper _mapper;
        public EmployeesController(IEmployeesService employeesService, IMapper mapper)
        {
            _employeesService = employeesService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var commentList = _employeesService.GetAll();
            var commentListDto = _mapper.Map<List<EmployeesDto>>(commentList);
            return CreateActionResult(ResponseDto<List<EmployeesDto>>.Success(commentListDto, 200));

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var commentSingularData = _employeesService.GetById(id);
            var commentSingularDto = _mapper.Map<EmployeesDto>(commentSingularData);
            return CreateActionResult(ResponseDto<EmployeesDto>.Success(commentSingularDto, 200));
        }
        [HttpPost]
        public IActionResult Add(EmployeesDto entity)
        {
            var entitydto = new Employees
            {
                isDeleted = false,
                CreatedDate = System.DateTime.Now,
                ModifyDate = System.DateTime.Now,
                departmanid = entity.departmanid,
                Surname = entity.Surname,
                StartDateOfWork = entity.StartDateOfWork,
                ExitEntryDate = entity.ExitEntryDate,
                FloorNo = entity.FloorNo,
                gender = entity.gender,
                hotelid = entity.hotelid,
                ImageUrl = entity.ImageUrl,
                Name = entity.Name,
                phoneNumber = entity.phoneNumber,
                isActive = entity.isActive,
            };
            _employeesService.Create(entitydto);
            return CreateActionResult(ResponseDto<EmployeesDto>.Success(200));
        }
        [HttpPost("{id}")]
        public IActionResult Delete(int id)
        {
            var commentSingularData = _employeesService.GetById(id);
            commentSingularData.isDeleted = true;
            commentSingularData.ModifyDate = System.DateTime.Now;
            _employeesService.Update(commentSingularData);
            return CreateActionResult(ResponseDto<Comment>.Success(200));
        }
        [HttpPost]
        public IActionResult Update(EmployeesDto entity)
        {
            var commentSingularData = _employeesService.GetById(entity.id);
            commentSingularData.isActive = entity.isActive;
            commentSingularData.ImageUrl = entity.ImageUrl;
            commentSingularData.ModifyDate = System.DateTime.Now;
            commentSingularData.Name = entity.Name;
            commentSingularData.phoneNumber = entity.phoneNumber;
            commentSingularData.StartDateOfWork = entity.StartDateOfWork;
            commentSingularData.Surname = entity.Surname;
            _employeesService.Update(commentSingularData);
            return CreateActionResult(ResponseDto<Comment>.Success(200));
        }
    }
}
