using AutoMapper;
using System.Linq;
using Worigo.Core.Dtos.Customer.Request;
using Worigo.Core.Dtos.Customer.Response;
using Worigo.Core.Dtos.HotelOfServiceDto.Response;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfCustomerDal : EfRepositoryDal<Customer,DataContext>, ICustomerDal
    {
        private readonly IMapper _mapper;
        private readonly IVerificationCodeDal _vertificationCodeDal;
        private readonly IServiceOfValueDal _serviceOfValueDal;

        public EfCustomerDal(IMapper mapper, IVerificationCodeDal vertificationCodeDal, IServiceOfValueDal serviceOfValueDal)
        {
            _mapper = mapper;
            _vertificationCodeDal = vertificationCodeDal;
            _serviceOfValueDal = serviceOfValueDal;
        }

        public ResponseDto<GetCustomerOfServicesResponse> GetCustomerOfServiceResponse(string code)
        {
            using (var db = new DataContext())
            {
                var data = _vertificationCodeDal.CodeForAccess(code);
                var customer = db.Customers.FirstOrDefault(x => x.Id == data.data.CustomerId);
                if (data == null)
                {
                    return new ResponseDto<GetCustomerOfServicesResponse>().Fail(400, "Data Not Found");
                }
                var query = from d1 in db.hotelOfServices
                            join d2 in db.Hotel on d1.HotelId equals d2.id
                            join d3 in db.Services on d1.ServiceId equals d3.id
                            select new HotelOfServiceResponse
                            {
                                HotelId = d2.id,
                                Service = d3.Name,
                                ServiceId = d3.id,
                                HotelName = d2.HotelName,
                                Id = d1.Id,
                            };

                var rsp = new GetCustomerOfServicesResponse
                {
                    hotelOfServiceResponses = query.ToList(),
                    CheckInTime = data.data.StartDate,
                    CheckOutTime = data.data.FinishDate,
                    FullName = customer != null ? string.Concat(customer.Name, " ", customer.Surname) : "",
                    HotelName = data.data.HotelName,
                    RoomId = data.data.roomid,
                    RoomNo = data.data.RoomNo,
                    VertificationId = data.data.id
                };
                return new ResponseDto<GetCustomerOfServicesResponse>().Success(rsp, 200);

            }
        }

        public ResponseDto<GetCustomerOfServiceValueResponse> GetCustomerOfServiceValueResponse(int serviceId, string code)
        {
            using (var db = new DataContext())
            {
                var data = _vertificationCodeDal.CodeForAccess(code);
                var customer = db.Customers.FirstOrDefault(x => x.Id == data.data.CustomerId);
                if (data == null)
                {
                    return new ResponseDto<GetCustomerOfServiceValueResponse>().Fail(400, "Data Not Found");
                }
                var serviceValue = _serviceOfValueDal.GetServiceValueOfHotelAndServiceId(serviceId, data.data.hotelid);
                var rsp = new GetCustomerOfServiceValueResponse
                {
                    serviceOfValueResponses = serviceValue.data,
                    CheckInTime = data.data.StartDate,
                    CheckOutTime = data.data.FinishDate,
                    FullName = customer.Name + " " + customer.Surname,
                    HotelName = data.data.HotelName,
                    RoomId = data.data.roomid,
                    RoomNo = data.data.RoomNo,
                    VertificationId = data.data.id
                };
                return new ResponseDto<GetCustomerOfServiceValueResponse>().Success(rsp, 200);
            }
        }

        public ResponseDto<CustomerResponse> PostCustomerByCode(CustomerAddOrUpdate request)
        {
            using (var db = new DataContext())
            {
                var data = db.vertificationCodes.Where(x => x.Phone == request.Phone && x.Code == request.Code && x.IsFull == false && x.isDeleted == false).FirstOrDefault();
                if (data == null)
                {
                    return new ResponseDto<CustomerResponse>().Fail(2, "Not Found");
                }


                request.VerificationId = data.id;
                var map = Create(_mapper.Map<Customer>(request));

                data.CustomerId = map.Id;
                _vertificationCodeDal.Update(data);

                var customerResponse = _mapper.Map<CustomerResponse>(map);

                customerResponse.Code = request.Code;
                customerResponse.HotelId = data.hotelid;

                return new ResponseDto<CustomerResponse>().Success(customerResponse, 200);

            }
        }
    }
}
