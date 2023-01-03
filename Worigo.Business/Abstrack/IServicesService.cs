using System.Collections.Generic;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.Services.Request;
using Worigo.Core.Dtos.Services.Response;

namespace Worigo.Business.Abstrack
{
    public interface IServicesService
    {

        ResponseDto<List<ServicesResponse>> GetServiceByHotelid(TokenKeys keys,int hotelid);
        ResponseDto<ServicesResponse> GetById(int id, TokenKeys keys);
        ResponseDto<ServicesResponse> Create(ServicesAddOrUpdateRequest request, TokenKeys keys);
        ResponseDto<ServicesResponse> Update(ServicesAddOrUpdateRequest request, TokenKeys keys);
    }
}
