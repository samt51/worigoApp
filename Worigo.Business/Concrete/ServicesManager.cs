using AutoMapper;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.HotelOfServiceDto.Request;
using Worigo.Core.Dtos.HotelOfServiceDto.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.Services.Request;
using Worigo.Core.Dtos.Services.Response;
using Worigo.Core.Extension;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class ServicesManager : IServicesService
    {
        private readonly IServicesDal _servicesDal;
        private readonly IHotelDal _hotelDal;
        private readonly IManagementOfHotelsDal _managementOfHotelsDal;
        private readonly IMapper _mapper;
        private readonly IServiceOfValueService _serviceOfValueService;
        private readonly IHotelOfServiceDal _hotelOfServiceDal;
        public ServicesManager(IHotelOfServiceDal hotelOfServiceDal, IServiceOfValueService serviceOfValueService, IServicesDal servicesDal, IMapper mapper, IHotelDal hotelDal, IManagementOfHotelsDal managementOfHotelsDal)
        {
            _servicesDal = servicesDal;
            _hotelDal = hotelDal;
            _managementOfHotelsDal = managementOfHotelsDal;
            _mapper = mapper;
            _serviceOfValueService = serviceOfValueService;
            _hotelOfServiceDal = hotelOfServiceDal;
        }
        public ResponseDto<ServicesResponse> Create(ServicesAddOrUpdateRequest request, TokenKeys keys)
        {
            _managementOfHotelsDal.AuthorizeControll(keys.role, keys.userId, 0, keys.companyid);
            request.ImageUrl = FileToByteConvert.FromFileToByte(request.file);
            var data = _servicesDal.Create(_mapper.Map<Services>(request));
            return new ResponseDto<ServicesResponse>().Success(_mapper.Map<ServicesResponse>(request), 200);
        }
        public ResponseDto<List<HotelOfServiceResponse>> GetServiceByHotelid(TokenKeys keys, int hotelid)
        {
            _managementOfHotelsDal.AuthorizeControll(keys.role, keys.userId, hotelid, keys.companyid);
            var response = _hotelOfServiceDal.GetServiceByHotelId(hotelid);
            return response;
        }
        public ResponseDto<ServicesResponse> GetById(int id, TokenKeys keys)
        {
            var service = _servicesDal.GetById(id);
            var hotel = _hotelDal.GetById(service.HotelId);
            if (keys.role == 3)
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(keys.userId, service.HotelId);

            if (keys.role == 2 && (keys.companyid == hotel.Companyid) && keys.role == 3 || keys.role == 1)
            {
                return new ResponseDto<ServicesResponse>().Success(_mapper.Map<ServicesResponse>(service), 200);
            }
            return new ResponseDto<ServicesResponse>().Authorization();
        }
        public ResponseDto<ServicesResponse> Update(ServicesAddOrUpdateRequest request, TokenKeys keys)
        {
            _managementOfHotelsDal.AuthorizeControll(keys.role, keys.userId, 0, keys.companyid);
            var data = _servicesDal.GetById(request.id);
            data.Name = request.Name;
            var update = _servicesDal.Update(data);
            return new ResponseDto<ServicesResponse>().Success(_mapper.Map<ServicesResponse>(request), 200);
        }
        public ResponseDto<List<ServicesResponse>> GetAllService(TokenKeys keys)
        {
            var data = _servicesDal.GetAll(x => x.isDeleted == false);
            var map = _mapper.Map<ResponseDto<List<ServicesResponse>>>(data);
            return new ResponseDto<List<ServicesResponse>>().Success(map.data, 200);
        }

        public ResponseDto<HotelOfServiceResponse> SelectService(TokenKeys keys, HotelOfServiceAddOrUpdate request)
        {
            var map = _mapper.Map<HotelOfService>(request);

            var entity = _hotelOfServiceDal.Create(map);
            return new ResponseDto<HotelOfServiceResponse>().Success(200);
        }

        public ResponseDto<HotelOfServiceResponse> RemoveServiceById(TokenKeys keys, int id)
        {
            var entity = _hotelOfServiceDal.GetById(id);
            var data = _hotelOfServiceDal.Delete(entity);
            return new ResponseDto<HotelOfServiceResponse>().Success(200);
        }
    }
}
