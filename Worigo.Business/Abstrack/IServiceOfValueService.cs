using System.Collections.Generic;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.ServiceOfValueDto.Request;
using Worigo.Core.Dtos.ServiceOfValueDto.Response;

namespace Worigo.Business.Abstrack
{
    public interface IServiceOfValueService
    {
        ResponseDto<ServiceOfValueResponse> PostServiceValueByHotelId(ServiceOfValueAddOrUpdate request, TokenKeys keys);
        ResponseDto<List<ServiceOfValueResponse>> GetServiceValueOfHotelAndServiceId(int serviceId, int hotelId, TokenKeys keys);


    }
}
