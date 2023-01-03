using AutoMapper;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
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
        public ServicesManager(IServicesDal servicesDal, IMapper mapper, IHotelDal hotelDal, IManagementOfHotelsDal managementOfHotelsDal)
        {
            _servicesDal = servicesDal;
            _hotelDal = hotelDal;
            _managementOfHotelsDal = managementOfHotelsDal;
            _mapper = mapper;
        }


        public ResponseDto<ServicesResponse> Create(ServicesAddOrUpdateRequest request, TokenKeys keys)
        {
            var hotel = _hotelDal.GetById(request.HotelId);
            if (keys.role == 3)
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(keys.userId, request.HotelId);

            if (keys.role == 2 && (keys.companyid == hotel.Companyid) && keys.role == 3 || keys.role == 1)
            {
                request.ImageUrl = FileToByteConvert.FromFileToByte(request.file);
                var data = _servicesDal.Create(_mapper.Map<Services>(request));
                return new ResponseDto<ServicesResponse>().Success(_mapper.Map<ServicesResponse>(request), 200);
            }
            return new ResponseDto<ServicesResponse>().Authorization();
        }

        public ResponseDto<List<ServicesResponse>> GetServiceByHotelid(TokenKeys keys, int hotelid)
        {
            var hotel = _hotelDal.GetById(hotelid);
            if (keys.role == 3)
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 3 || keys.role == 1)
            {
                var response = _mapper.Map<List<ServicesResponse>>(_servicesDal.GetAll(x => x.HotelId == hotelid && x.isDeleted == false));
                return new ResponseDto<List<ServicesResponse>>().Success(response, 200);
            }
            return new ResponseDto<List<ServicesResponse>>().Authorization();
        }

 

        public ResponseDto<ServicesResponse> GetById(int id, TokenKeys keys)
        {
            var service=_servicesDal.GetById(id);
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
            var data = _servicesDal.GetById(request.id);
            var hotel = _hotelDal.GetById(request.HotelId); 
            if (keys.role == 3)
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(keys.userId, request.HotelId);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) && keys.role == 3 || keys.role == 1)
            {
                data.Name = request.Name;
                var update = _servicesDal.Update(data);
                return new ResponseDto<ServicesResponse>().Success(_mapper.Map<ServicesResponse>(request), 200);
            }
            return new ResponseDto<ServicesResponse>().Authorization();
        }
    }
}
