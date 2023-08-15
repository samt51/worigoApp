using AutoMapper;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.ServicesValue.Request;
using Worigo.Core.Dtos.ServicesValue.Response;
using Worigo.Core.Extension;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class ServiceValueManager : IServiceValueService
    {
        private readonly IServicesValuesDal _servicesValuesDal;
        private readonly IMapper _mapper;
        private readonly IHotelDal _hotelDal;
        private readonly IHotelService _hotelService;
        private readonly IServicesDal _servicesDal;
        private readonly IManagementOfHotelsDal _managementOfHotelsDal;
        public ServiceValueManager(IHotelService hotelService, IServicesValuesDal servicesValuesDal, IHotelDal hotelDal, IServicesDal servicesDal, IManagementOfHotelsDal managementOfHotelsDal)
        {
            _servicesValuesDal = servicesValuesDal;
            _hotelDal = hotelDal;
            _servicesDal = servicesDal;
            _managementOfHotelsDal = managementOfHotelsDal;
            _hotelService = hotelService;
        }

        public ResponseDto<ServicesValueResponse> Create(ServicesValuesAddOrUpdateRequest request, TokenKeys keys)
        {
            var service = _servicesDal.GetById(request.Serviceid);
            var hotel = _hotelDal.GetById(service.HotelId);
            if (keys.role == 3)
            {
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(keys.userId, service.HotelId);
            }
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 3 || keys.role == 1)
            {
                request.ImageUrl = FileToByteConvert.FromFileToByte(request.file);
                var data = _servicesValuesDal.Create(_mapper.Map<ServicesValues>(request));
                return new ResponseDto<ServicesValueResponse>().Success(_mapper.Map<ServicesValueResponse>(data), 200);
            }
            return new ResponseDto<ServicesValueResponse>().Authorization();
        }
        public ResponseDto<ServicesValueResponse> GetById(int id, TokenKeys keys)
        {
            var map = _mapper.Map<ServicesValueResponse>(_servicesValuesDal.GetById(id));
            return new ResponseDto<ServicesValueResponse>().Success(map, 200);
        }
        public ResponseDto<List<ServicesValueResponse>> GetValueByServiceId(int hotelId, int serviceid, TokenKeys keys)
        {
            _managementOfHotelsDal.AuthorizeControll(keys.role, keys.userId, hotelId, keys.companyid);
            return new ResponseDto<List<ServicesValueResponse>>().Success(_servicesValuesDal.GetValueByServiceId(serviceid, hotelId), 200);
        }
        public ResponseDto<ServicesValueResponse> Update(ServicesValuesAddOrUpdateRequest request, TokenKeys keys)
        {
            var value = _servicesValuesDal.GetById(request.id);
            var service = _servicesDal.GetById(request.Serviceid);
            var hotel = _hotelDal.GetById(service.HotelId);
            if (keys.role == 3)
            {
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(keys.userId, service.HotelId);
            }
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 3 || keys.role == 1)
            {
                value.value = request.value;
                value.Serviceid = request.Serviceid;
                var data = _servicesValuesDal.Create(value);
                return new ResponseDto<ServicesValueResponse>().Success(_mapper.Map<ServicesValueResponse>(data), 200);
            }
            return new ResponseDto<ServicesValueResponse>().Authorization();
        }
    }
}
