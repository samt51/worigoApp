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

    public class ServicesController : CustomBaseController
    {
        private readonly IServicesService _servicesService;
        private IMapper _mapper;
        public ServicesController(IServicesService servicesService, IMapper mapper)
        {
            _servicesService = servicesService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll(int hotelid)
        {
            var services = _servicesService.serviceAndHotelJoins(hotelid);
          
            return CreateActionResult(ResponseDto<List<ServiceAndHotelJoin>>.Success(services, 200));
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var serviceSingular = _servicesService.GetById(id);
            var serviceDto = _mapper.Map<ServicesDto>(serviceSingular);
            return CreateActionResult(ResponseDto<ServicesDto>.Success(serviceDto, 200));
        }
        [HttpPost]
        public IActionResult Add(ServicesDto entity)
        {
            var entitydto = new Services
            {
                isDeleted=false,
                ModifyDate=System.DateTime.Now,
                CreatedDate=System.DateTime.Now,
                hotelid = entity.Hotelid,
                Name=entity.Name,
                isActive=true
            };
            _servicesService.Create(_mapper.Map<Services>(entitydto));
            return CreateActionResult(ResponseDto<Services>.Success(200));
        }
        [HttpPost("{id}")]
        public IActionResult Delete(int id)
        {
            var serviceSingularData = _servicesService.GetById(id);
            serviceSingularData.isDeleted = true;
            _servicesService.Update(serviceSingularData);
            return CreateActionResult(ResponseDto<Services>.Success(200));
        }
        [HttpPost]
        public IActionResult Update(ServicesDto entity)
        {
            var serviceupdate = _servicesService.GetById(entity.Id);
            serviceupdate.ModifyDate = System.DateTime.Now;
            serviceupdate.hotelid = entity.Hotelid;
            serviceupdate.Name = entity.Name;
            return CreateActionResult(ResponseDto<Services>.Success(200));
        }
    }
}
