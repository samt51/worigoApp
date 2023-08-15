using System.Collections.Generic;
using Worigo.Core.Dtos.HotelOfServiceDto.Request;
using Worigo.Core.Dtos.HotelOfServiceDto.Response;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface IHotelOfServiceDal : IRepositoryDesignPattern<HotelOfService>
    {
        ResponseDto<HotelOfServiceResponse> PostServiceByHotelId(HotelOfServiceAddOrUpdate request);
        ResponseDto<List<HotelOfServiceResponse>> GetServiceByHotelId(int hotelId);
    }
}
