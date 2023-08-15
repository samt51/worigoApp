using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.HotelOfServiceDto.Request;
using Worigo.Core.Dtos.HotelOfServiceDto.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.DataAccess.Abstrack;

namespace Worigo.Business.Concrete
{
    public class HotelOfServiceManager : IHotelOfServicesService
    {
        private readonly IHotelOfServiceDal _hotelOfServiceDal;
        private readonly IManagementOfHotelService _managementOfHotelService;

        public HotelOfServiceManager(IHotelOfServiceDal hotelOfServiceDal, IManagementOfHotelService managementOfHotelService)
        {
            _hotelOfServiceDal = hotelOfServiceDal;
            _managementOfHotelService = managementOfHotelService;
        }

        public ResponseDto<List<HotelOfServiceResponse>> GetServiceByHotelId(int hotelId, TokenKeys keys)
        {
            _managementOfHotelService.AuthorizeControll(keys.role, keys.userId, hotelId, keys.companyid);
            return _hotelOfServiceDal.GetServiceByHotelId(hotelId);
        }

        public ResponseDto<HotelOfServiceResponse> PostServiceByHotelId(HotelOfServiceAddOrUpdate request, TokenKeys keys)
        {
            _managementOfHotelService.AuthorizeControll(keys.role, keys.userId, request.HotelId, keys.companyid);
            return _hotelOfServiceDal.PostServiceByHotelId(request);
        }
    }
}
