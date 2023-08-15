using System.Collections.Generic;
using Worigo.Core.Dtos.Order.Request;
using Worigo.Core.Dtos.Order.Response;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface IOrderDal : IRepositoryDesignPattern<Order>
    {
        ResponseDto<List<OrderResponse>> GetOrderByHotelId(int hotelid);
        ResponseDto<List<OrderResponse>> GetOrderByStatus(int vertificationId, int status);
        ResponseDto<OrderResponse> PostOrder(OrderAddOrUpdateRequest request);
        ResponseDto<OrderResponse> UpdateOrder(OrderAddOrUpdateRequest request);


    }
}
