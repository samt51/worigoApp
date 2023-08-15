using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.Order.Request;
using Worigo.Core.Dtos.Order.Response;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.DataAccess.Abstrack;

namespace Worigo.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;
        private readonly IHotelService _hotelService;
        private readonly IManagementOfHotelService _managementOfHotelService;
        public OrderManager(IOrderDal orderDal, IHotelService hotelService, IManagementOfHotelService managementOfHotelService)
        {
            _orderDal = orderDal;
            _hotelService = hotelService;
            _managementOfHotelService = managementOfHotelService;
        }
        public ResponseDto<List<OrderResponse>> GetOrderByHotelId(int hotelid, TokenKeys keys)
        {
            var hotel = _hotelService.GetById(keys, hotelid);
            var data = _orderDal.GetOrderByHotelId(hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.data.Companyid) || keys.role == 1)
            {
                return new ResponseDto<List<OrderResponse>>().Success(data.data, 200);
            }
            else if (keys.role == 3)
            {
                _managementOfHotelService.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                return new ResponseDto<List<OrderResponse>>().Success(data.data, 200);
            }
            return new ResponseDto<List<OrderResponse>>().Authorization();
        }
        public ResponseDto<List<OrderResponse>> GetOrderByStatus(int vertificationId, int status, TokenKeys keys)
        {
            _managementOfHotelService.AuthorizeControll(keys.role, keys.userId, 0, keys.companyid);
            return _orderDal.GetOrderByStatus(vertificationId, status);
        }
        public ResponseDto<OrderResponse> PostOrder(OrderAddOrUpdateRequest request, TokenKeys keys)
        {
            _managementOfHotelService.AuthorizeControll(keys.role, keys.userId, request.HotelId, keys.companyid);
            return _orderDal.PostOrder(request);
        }
        public ResponseDto<OrderResponse> UpdateOrder(OrderAddOrUpdateRequest request, TokenKeys keys)
        {
            _managementOfHotelService.AuthorizeControll(keys.role, keys.userId, request.HotelId, keys.companyid);
            return _orderDal.UpdateOrder(request);
        }
    }
}
