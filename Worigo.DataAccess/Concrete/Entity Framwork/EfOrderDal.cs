using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.Order.Request;
using Worigo.Core.Dtos.Order.Response;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfOrderDal : EfRepositoryDal<Order, DataContext>, IOrderDal
    {
        private readonly IMapper _mapper;
        public EfOrderDal(IMapper mapper)
        {
            _mapper = mapper;
        }
        public ResponseDto<List<OrderResponse>> GetOrderByHotelId(int hotelid)
        {
            using (var db = new DataContext())
            {
                var query = from d1 in db.vertificationCodes.Where(x => x.hotelid == hotelid)
                            join d2 in db.Order on d1.id equals d2.VertificationId
                            join d3 in db.Employees on d2.EmployeeId equals d3.id
                            join d4 in db.employeesType on d3.employeestypeid equals d4.id
                            join d5 in db.ServicesValues on d2.serviceValueId equals d5.id
                            join d6 in db.Services on d5.Serviceid equals d6.id
                            join d7 in db.Room on d1.roomid equals d7.id
                            select new OrderResponse
                            {
                                id = d2.id,
                                EmployeeFullName = string.Concat(d3.Name, " ", d3.Surname),
                                EmployeeId = d3.id,
                                serviceId = d6.id,
                                Service = d6.Name,
                                serviceValueId = d5.id,
                                ServiceValue = d5.value,
                                Status = d2.Status,
                                StatusName = d2.Status == 1 ? "Aktif" : d2.Status == 2 ? "Tamamlanmış" : d2.Status == 3 ? "İptal" : "",
                                CancelDescription = d2.Description,
                                CompletedDate = d2.CompletedDate,
                                orderDate = d2.orderDate,
                                EmployeeImageUrl = d3.ImageUrl,
                                EmployeePositionName = d4.TypeName,
                                hotelid = d1.hotelid,
                                Quantity = d2.Quantity,
                                RoomNo = d7.RoomNo.ToString(),
                                totalPrice = d2.totalPrice,
                                unitPrice = d2.unitPrice,
                            };
                return new ResponseDto<List<OrderResponse>>().Success(query.ToList(), 200);



            }
        }

        public ResponseDto<List<OrderResponse>> GetOrderByStatus(int vertificationId, int status)
        {
            using (var db = new DataContext())
            {
                var query = from d1 in db.vertificationCodes.Where(x => x.id == vertificationId)
                            join d2 in db.Order on d1.id equals d2.VertificationId
                            join d3 in db.Employees on d2.EmployeeId equals d3.id
                            join d4 in db.employeesType on d3.employeestypeid equals d4.id
                            join d5 in db.ServicesValues on d2.serviceValueId equals d5.id
                            join d6 in db.Services on d5.Serviceid equals d6.id
                            join d7 in db.Room on d1.roomid equals d7.id
                            where d2.Status == status
                            select new OrderResponse
                            {
                                id = d2.id,
                                EmployeeFullName = string.Concat(d3.Name, " ", d3.Surname),
                                EmployeeId = d3.id,
                                serviceId = d6.id,
                                Service = d6.Name,
                                serviceValueId = d5.id,
                                ServiceValue = d5.value,
                                Status = d2.Status,
                                StatusName = d2.Status == 1 ? "Aktif" : d2.Status == 2 ? "Tamamlanmış" : d2.Status == 3 ? "İptal" : "",
                                CancelDescription = d2.Description,
                                CompletedDate = d2.CompletedDate,
                                orderDate = d2.orderDate,
                                EmployeeImageUrl = d3.ImageUrl,
                                EmployeePositionName = d4.TypeName,
                                hotelid = d1.hotelid,
                                Quantity = d2.Quantity,
                                RoomNo = d7.RoomNo.ToString(),
                                totalPrice = d2.totalPrice,
                                unitPrice = d2.unitPrice,
                            };
                return new ResponseDto<List<OrderResponse>>().Success(query.ToList(), 200);



            }
        }

        public ResponseDto<OrderResponse> PostOrder(OrderAddOrUpdateRequest request)
        {
            var map = _mapper.Map<Order>(request);
            var create = Create(map);
            return new ResponseDto<OrderResponse>().Success(200);
        }

        public ResponseDto<OrderResponse> UpdateOrder(OrderAddOrUpdateRequest request)
        {
            var map = _mapper.Map<Order>(request);
            var create = Update(map);
            return new ResponseDto<OrderResponse>().Success(200);
        }
    }
}
