using System.Collections.Generic;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.Order.Request;
using Worigo.Core.Dtos.Order.Response;
using Worigo.Core.Dtos.ResponseDtos;

namespace Worigo.Business.Abstrack
{
    public interface IOrderService
    {
        ResponseDto<List<OrderResponse>> GetOrderByHotelId(int hotelid, TokenKeys keys);
        ResponseDto<List<OrderResponse>> GetOrderByStatus(int vertificationId, int status, TokenKeys keys);
        ResponseDto<OrderResponse> PostOrder(OrderAddOrUpdateRequest request, TokenKeys keys);
        ResponseDto<OrderResponse> UpdateOrder(OrderAddOrUpdateRequest request, TokenKeys keys);

    }
}
