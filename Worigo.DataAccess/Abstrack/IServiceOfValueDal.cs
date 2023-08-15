using System.Collections.Generic;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.ServiceOfValueDto.Request;
using Worigo.Core.Dtos.ServiceOfValueDto.Response;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface IServiceOfValueDal : IRepositoryDesignPattern<ServiceOfValues>
    {
        ResponseDto<ServiceOfValueResponse> PostServiceValueByHotelId(ServiceOfValueAddOrUpdate request);
        ResponseDto<List<ServiceOfValueResponse>> GetServiceValueOfHotelAndServiceId(int serviceId, int hotelId);
    }
}
