using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.Order.Request;
using Worigo.Core.Dtos.Order.Response;
using Worigo.Core.Dtos.ResponseDtos;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class OrderController : CustomBaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet("{hotelid}")]
        public ResponseDto<List<OrderResponse>> GetOrderByHotelId([FromHeader] string Authorization, int hotelid)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _orderService.GetOrderByHotelId(hotelid, keys);
        }
        [HttpGet("{vertificationId}/{status}")]
        public ResponseDto<List<OrderResponse>> GetOrderByStatus([FromHeader] string Authorization, int vertificationId,int status)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _orderService.GetOrderByStatus(vertificationId, status, keys);
        }
        [HttpPost]
        public ResponseDto<OrderResponse> PostOrder([FromHeader] string Authorization, OrderAddOrUpdateRequest request)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _orderService.PostOrder(request,keys);
        }
        [HttpPost]
        public ResponseDto<OrderResponse> UpdateOrder([FromHeader] string Authorization, OrderAddOrUpdateRequest request)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _orderService.UpdateOrder(request, keys);
        }
    }
}
