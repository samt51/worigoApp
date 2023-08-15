using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.HotelOfServiceDto.Request;
using Worigo.Core.Dtos.HotelOfServiceDto.Response;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfHotelOfServiceDal : EfRepositoryDal<HotelOfService, DataContext>, IHotelOfServiceDal
    {
        private readonly IMapper _mapper;

        public EfHotelOfServiceDal(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ResponseDto<List<HotelOfServiceResponse>> GetServiceByHotelId(int hotelId)
        {
            using (var db = new DataContext())
            {
                var query = from d1 in db.hotelOfServices
                            join d2 in db.Services on d1.ServiceId equals d2.id
                            join d3 in db.Hotel on d1.HotelId equals d3.id
                            where d1.isDeleted == false && d1.HotelId == hotelId
                            select new HotelOfServiceResponse
                            {
                                Id = d1.Id,
                                ServiceId = d2.id,
                                Service = d2.Name,
                                HotelId = d1.HotelId,
                                HotelName = d3.HotelName,
                                IsActive = true
                            };
                var queryList = query.ToList();
                var allService = db.Services.Where(x => x.isActive == true && x.isDeleted == false).ToList();
                foreach (var item in allService)
                {
                    var data = queryList.Where(x => x.ServiceId == item.id).FirstOrDefault();
                    if (data == null)
                    {
                        queryList.Add(new HotelOfServiceResponse
                        {
                            ServiceId = item.id,
                            HotelId = 0,
                            HotelName = "",
                            Service = item.Name,
                            IsActive = false
                        });
                    }
                }

                return new ResponseDto<List<HotelOfServiceResponse>>().Success(queryList.ToList(), 200);
            }
        }

        public ResponseDto<HotelOfServiceResponse> PostServiceByHotelId(HotelOfServiceAddOrUpdate request)
        {
            using (var db = new DataContext())
            {
                var data = Create(_mapper.Map<HotelOfService>(request));
                var query = from d1 in db.ServiceOfValues
                            join d2 in db.Hotel on d1.HotelId equals d2.id
                            join d3 in db.Services on d1.ServiceId equals d3.id
                            where d1.id == data.Id
                            select new HotelOfServiceResponse
                            {
                                Id = d1.id,
                                Service = d3.Name,
                                ServiceId = d3.id,
                                HotelId = d2.id,
                                HotelName = d2.HotelName
                            };
                return new ResponseDto<HotelOfServiceResponse>().Success(query.FirstOrDefault(), 200);
            }
        }
    }
}
