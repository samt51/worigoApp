using System.Collections.Generic;
using Worigo.Core.Dtos.Hotel.Request;
using Worigo.Core.Dtos.Hotel.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;

namespace Worigo.Business.Abstrack
{
    public interface IHotelService
    {
        ResponseDto<List<HotelResponse>> GetHotelByCompanyid(int companyid, TokenKeys keys);
        ResponseDto<HotelResponse> GetHotelByCompanyIdAndHotelId(int companyid, int hotelid, TokenKeys keys);
        ResponseDto<List<HotelResponse>> GetAll(TokenKeys keys);
        ResponseDto<HotelResponse> GetById(TokenKeys token, int id);
        ResponseDto<HotelResponse> Create(TokenKeys data, HotelAddOrUpdateRequest entity);
        ResponseDto<HotelResponse> Update(HotelAddOrUpdateRequest entity, TokenKeys keys);
    }
}
