using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.ServiceOfValueDto.Request;
using Worigo.Core.Dtos.ServiceOfValueDto.Response;
using Worigo.DataAccess.Abstrack;

namespace Worigo.Business.Concrete
{
    public class ServiceOfValueManager : IServiceOfValueService
    {
        private readonly IServiceOfValueDal _serviceOfValueDal;
        private readonly IManagementOfHotelService _managementOfHotelService;
        public ServiceOfValueManager(IServiceOfValueDal serviceOfValueDal, IManagementOfHotelService managementOfHotelService)
        {
            _serviceOfValueDal = serviceOfValueDal;
            _managementOfHotelService = managementOfHotelService;
        }

        public ResponseDto<List<ServiceOfValueResponse>> GetServiceValueOfHotelAndServiceId(int serviceId, int hotelId, TokenKeys keys)
        {
            _managementOfHotelService.AuthorizeControll(keys.role, keys.userId, hotelId, keys.companyid);
            return _serviceOfValueDal.GetServiceValueOfHotelAndServiceId(serviceId, hotelId);
        }

        public ResponseDto<ServiceOfValueResponse> PostServiceValueByHotelId(ServiceOfValueAddOrUpdate request, TokenKeys keys)
        {
            _managementOfHotelService.AuthorizeControll(keys.role, keys.userId, request.HotelId, keys.companyid);
            return _serviceOfValueDal.PostServiceValueByHotelId(request);
        }
    }
}
