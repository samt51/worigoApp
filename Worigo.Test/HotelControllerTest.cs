using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Worigo.API.Controllers;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Entity.Concrete;
using Xunit;

namespace Worigo.Test
{
    public class HotelControllerTest
    {
        private readonly Mock<IHotelService> _mock;
        private readonly HotelController _hotelController;
        private readonly Mock<IMapper> _mapper;
        private List<Hotel> hotels;
        private List<HotelDto> hotelDtos;
        private HotelDto HotelDto;
        Hotel hotel = null;
        HotelDto hotelDto = null;
        public HotelControllerTest()
        {
            _mock = new Mock<IHotelService>();
            _mapper = new Mock<IMapper>();
            hotel = null;
            hotelDto = null;
            _hotelController = new HotelController(_mock.Object, _mapper.Object);
            hotels = new List<Hotel> { new Hotel { HotelName = "", Adress = "", ImageUrl = "", PhoneNumber = "", id = 1, NumberOfStar = 3, Email = "adsa" }, new Hotel { HotelName = "", PhoneNumber = "", id = 2, Adress = "dsadas", ImageUrl = "1.jpg" } };
            hotelDtos = new List<HotelDto>();
            foreach (var item in hotels)
            {
                var hotel = new HotelDto
                {
                    id = item.id,
                    HotelName = item.HotelName,
                    PhoneNumber = item.PhoneNumber,
                    NumberOfStar = item.NumberOfStar,
                    Email = item.Email,
                    Adress = item.Adress,
                    ImageUrl = item.ImageUrl
                };
                hotelDtos.Add(hotel);
            }
        }
        [Fact]
        public void List_ActionExcecutes_ReturnView()
        {
            var result = _hotelController.GetAll();
            Assert.IsType<ObjectResult>(result);
        }
        [Fact]
        public void GetAll_ActionExcecute_ReturnHotelList()
        {
            //arrange
            _mock.Setup(rep => rep.GetAll()).Returns(hotels);
            _mapper.Setup(m => m.Map<IEnumerable<HotelDto>>(It.IsAny<List<Hotel>>())).Returns(hotelDtos);
            //act
            var result = _hotelController.GetAll();
            //assert
            var objectresult = Assert.IsType<ObjectResult>(result);
            var hotellist = Assert.IsAssignableFrom<ResponseDto<List<HotelDto>>>(objectresult.Value);
            Assert.Equal<int>(2, hotellist.Data.Count);
        }
        [Theory]
        [InlineData(0)]
        public void GetById_IdInValid_ReturnNotFound(int hotelid)
        {

            _mock.Setup(z => z.GetById(hotelid)).Returns(hotel);
            _mapper.Setup(x => x.Map<HotelDto>(It.IsAny<Hotel>())).Returns(hotelDto);
            var result = _hotelController.GetById(hotelid);
            Assert.IsType<ObjectResult>(result);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetHotel_IdValid_ReturnOkResult(int hotelid)
        {
            var product = hotels.First(x => x.id == hotelid);
            var hoteldto = hotelDtos.First(x => x.id == hotelid);
            _mock.Setup(x => x.GetById(hotelid)).Returns(product);
            _mapper.Setup(x => x.Map<HotelDto>(It.IsAny<Hotel>())).Returns(hoteldto);


            var result = _hotelController.GetById(hotelid);

            var okresult = Assert.IsType<ObjectResult>(result);
            var returnresult = Assert.IsType<ResponseDto<HotelDto>>(okresult.Value);
            Assert.Equal(hotelid, returnresult.Data.id);

        }

        [Fact]
        public void Update_IdValid_ReturnNotFound()
        {

            var hoteldto = hotelDtos.First();
            var hotel = hotels.First();
            var productbyid = hotels.First(x => x.id == hotel.id);
            var hoteldtobyid = hotelDtos.First(x => x.id == hoteldto.id);
            _mock.Setup(x => x.GetById(hotel.id)).Returns(hotel);
            _mapper.Setup(x => x.Map<HotelDto>(It.IsAny<Hotel>())).Returns(hoteldto);
            _mock.Setup(x => x.Update(hotel));
            _mapper.Setup(x => x.Map<HotelDto>(It.IsAny<Hotel>()));
            var result = _hotelController.Update(hoteldto);
            _mock.Verify(x => x.Update(hotel), Times.Once);
            Assert.IsType<ObjectResult>(result);

        }
        [Fact]
        public void Add_ActionExecutes_ReturnCreateAtAction()
        {
            var hotelentity = hotels.First();
            var hoteldtoentity = hotelDtos.First();
            _mock.Setup(x => x.Create(hotelentity));
            _mapper.Setup(x => x.Map<HotelDto>(It.IsAny<Hotel>())).Returns(hoteldtoentity);

            var result = _hotelController.Add(hoteldtoentity);


            var key = Assert.IsType<ObjectResult>(result);

            var returnresult = Assert.IsType<ResponseDto<Hotel>>(key.Value);



            Assert.Equal(200, returnresult.StatusCode);


        }

    }
}
