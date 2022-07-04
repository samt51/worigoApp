using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Entity.Concrete;
using Worigo.Core.Dtos.ListDto;
namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class HotelController : CustomBaseController
    {
        private readonly IHotelService _hotelService;
        private readonly IMapper _mapper;
        public HotelController(IHotelService hotelService, IMapper mapper)
        {
            _hotelService = hotelService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var hotelall = _hotelService.GetAll();
            var hoteldto = _mapper.Map<List<HotelDto>>(hotelall);
            return CreateActionResult(ResponseDto<List<HotelDto>>.Success(hoteldto, 200));
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var hotelSingular = _hotelService.GetById(id);
            var hotelsingulardto = _mapper.Map<HotelDto>(hotelSingular);
            return CreateActionResult(ResponseDto<HotelDto>.Success(hotelsingulardto, 200));
        }
        [HttpPost]
        public IActionResult Add(HotelDto hoteldto)
        {
            var dtoadd = new Hotel
            {
                CreatedDate = System.DateTime.Now,
                ModifyDate = System.DateTime.Now,
                isDeleted = false,
                NumberOfStar = hoteldto.NumberOfStar,
                Adress = hoteldto.Adress,
                Email = hoteldto.Email,
                PhoneNumber = hoteldto.PhoneNumber,
                HotelName = hoteldto.HotelName,
                ImageUrl = hoteldto.ImageUrl,
                isActive=hoteldto.isActive,
      
            };
            _hotelService.Create(dtoadd);
            return CreateActionResult(ResponseDto<Hotel>.Success(200));
        }
        [HttpPost("{id}")]
        public IActionResult Delete(int id)
        {
            var hotelsingular = _hotelService.GetById(id);
            hotelsingular.isDeleted = true;
            _hotelService.Update(hotelsingular);
            return CreateActionResult(ResponseDto<Hotel>.Success(200));
        }
        [HttpPost]
        public IActionResult Update(HotelDto hotels)
        {
            var singularHotel = _hotelService.GetById(hotels.id);
            singularHotel.HotelName = hotels.HotelName;
            singularHotel.ImageUrl = hotels.ImageUrl;
            singularHotel.Adress = hotels.Adress;
            singularHotel.PhoneNumber = hotels.PhoneNumber;
            singularHotel.Email = hotels.Email;
            singularHotel.NumberOfStar = hotels.NumberOfStar;
            singularHotel.ModifyDate = System.DateTime.Now;
            singularHotel.isActive = hotels.isActive;
            _hotelService.Update(singularHotel);
            return CreateActionResult(ResponseDto<Hotel>.Success(200));
        }
    }
}
