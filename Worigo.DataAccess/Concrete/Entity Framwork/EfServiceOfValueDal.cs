using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.ServiceOfValueDto.Request;
using Worigo.Core.Dtos.ServiceOfValueDto.Response;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfServiceOfValueDal : EfRepositoryDal<ServiceOfValues, DataContext>, IServiceOfValueDal
    {
        private readonly IMapper _mapper;

        public EfServiceOfValueDal(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ResponseDto<List<ServiceOfValueResponse>> GetServiceValueOfHotelAndServiceId(int serviceId, int hotelId)
        {
            using (var db = new DataContext())
            {

                var query = from d1 in db.ServiceOfValues
                            join d2 in db.Services on d1.ServiceId equals d2.id
                            join d3 in db.ServicesValues on d1.ServiceValueId equals d3.id
                            join d4 in db.Hotel on d1.HotelId equals d4.id
                            where d1.ServiceId == serviceId && d1.HotelId == hotelId && d1.isDeleted == false
                            select new ServiceOfValueResponse
                            {
                                id = d1.id,
                                ServiceId = d1.ServiceId,
                                ServiceValueId = d1.ServiceValueId,
                                Service = d2.Name,
                                ServiceValue = d2.Name,
                                HotelId = d4.id,
                                HotelName = d4.HotelName
                            };
                return new ResponseDto<List<ServiceOfValueResponse>>().Success(query.ToList(), 200);
            }
        }

        public ResponseDto<ServiceOfValueResponse> PostServiceValueByHotelId(ServiceOfValueAddOrUpdate request)
        {
            using (var db = new DataContext())
            {
                var data = Create(_mapper.Map<ServiceOfValues>(request));

                var query = from d1 in db.ServiceOfValues
                            join d2 in db.Services on d1.ServiceId equals d2.id
                            join d3 in db.ServicesValues on d1.ServiceValueId equals d3.id
                            join d4 in db.Hotel on d1.HotelId equals d4.id
                            where d1.id == data.id && data.isDeleted == false
                            select new ServiceOfValueResponse
                            {
                                id = d1.id,
                                ServiceId = d2.id,
                                Service = d2.Name,
                                ServiceValue = d3.value,
                                ServiceValueId = d3.id,
                                HotelId = d4.id,
                                HotelName = d4.HotelName
                            };
                return new ResponseDto<ServiceOfValueResponse>().Success(query.FirstOrDefault(), 200);
            }
        }
    }
}
