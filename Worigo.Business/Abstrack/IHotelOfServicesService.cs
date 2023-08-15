using System.Collections.Generic;
using Worigo.Core.Dtos.HotelOfServiceDto.Request;
using Worigo.Core.Dtos.HotelOfServiceDto.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;

namespace Worigo.Business.Abstrack
{
    public interface IHotelOfServicesService
    {
        ResponseDto<HotelOfServiceResponse> PostServiceByHotelId(HotelOfServiceAddOrUpdate request, TokenKeys keys);
        ResponseDto<List<HotelOfServiceResponse>> GetServiceByHotelId(int hotelId, TokenKeys keys);
    }
}
