using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.Order.Request;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CustomerController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IHotelService _hotelService;
        private readonly IRoomService _roomService;
        private readonly IOrderService _orderService;
        private readonly IEmployeesService _employeesService;
        private readonly IServiceValueOfEmployeeTypeService _serviceValueOfEmployeeTypeService;
        public CustomerController(IOrderService orderService, 
            IServiceValueOfEmployeeTypeService serviceValueOfEmployeeTypeService,
            IMapper mapper, IRoomService roomService, IHotelService hotelService, IEmployeesService employeesService)
        {
            _mapper = mapper;
            _roomService = roomService;
            _hotelService = hotelService;
            _orderService = orderService;
            _serviceValueOfEmployeeTypeService = serviceValueOfEmployeeTypeService;
            _employeesService = employeesService;   
        }
        [HttpPost]
        public IActionResult AddNewOrder([FromHeader] string Authorization, OrderRequestDto orderRequestDto)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var hotel = _hotelService.GetById(keys, orderRequestDto.hotelid);
            if (keys.companyid == hotel.Companyid)
            {
                var list = _serviceValueOfEmployeeTypeService.GetDataByServiceValueId(orderRequestDto.serviceValueId, orderRequestDto.hotelid);
                foreach (var item in list)
                {
                    var employee = _employeesService.GetEmployeeByEmployeeTypeId(item.employeetypeid, orderRequestDto.hotelid);
                    if (employee.Count > 1)
                    {
                        foreach (var emp in employee)
                        {

                        }
                    }
                }
                var entity = _mapper.Map<Order>(orderRequestDto);
                _orderService.Create(entity);
                return CreateActionResult(ResponseDto<NoContentResult>.Success(200));
            }
            return CreateActionResult(ResponseDto<NoContentResult>.Authorization());
        }
    }
}
