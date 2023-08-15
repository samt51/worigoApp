using AutoMapper;
using System;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.Reports.Response;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.VerificationCodeDto.Dto;
using Worigo.Core.Dtos.VerificationCodeDto.Request;
using Worigo.Core.Dtos.VerificationCodeDto.Response;
using Worigo.Core.Extension;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class VerificationCodeManager : IVerificationCodeService
    {
        private readonly IVerificationCodeDal _vertificationCodeDal;
        private readonly IHotelService _hotelService;
        private readonly IManagementOfHotelsDal _managementOfHotelsDal;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public VerificationCodeManager(IUserService userService, IVerificationCodeDal vertificationCodeDal, IHotelService hotelService, IMapper mapper, IManagementOfHotelsDal managementOfHotelsDal)
        {
            _vertificationCodeDal = vertificationCodeDal;
            _mapper = mapper;
            _hotelService = hotelService;
            _managementOfHotelsDal = managementOfHotelsDal;
            _userService = userService;
        }

        public ResponseDto<VerificationCodeResponse> CodeForAccess(string code)
        {
            return _vertificationCodeDal.CodeForAccess(code);
        }

        public ResponseDto<VerificationCodeForAccessDto> CodeForAccess(ForAccessRequestModel requestModel)
        {
            var data = _vertificationCodeDal.CodeForAccess(requestModel);
            if (data.data == null)
            {
                return new ResponseDto<VerificationCodeForAccessDto>().Success(data.data, 200);
            }

            var token = _userService.ProduceToken(data.data.id.ToString(), data.data.Email, 6.ToString(), data.data.CompanyId.ToString(), requestModel.DeviceId, requestModel.Lng);
            data.data.Token = token;
            return new ResponseDto<VerificationCodeForAccessDto>().Success(data.data, 200);

        }

        public ResponseDto<VerificationCodeResponse> Create(VerificationCodeRequest entity, TokenKeys keys)
        {

            if (keys.companyid == _hotelService.GetById(keys, entity.hotelid).data.Companyid || keys.role == 1)
            {
                entity.Code = CodeRandomGeneration.RandomVertificationCodeCreate().ToString();
                var response = _vertificationCodeDal.Create(_mapper.Map<VerificationCodes>(entity));
                var mapResponse = _mapper.Map<VerificationCodeResponse>(response);
                return new ResponseDto<VerificationCodeResponse>().Success(mapResponse, 200);
            }
            return new ResponseDto<VerificationCodeResponse>().Authorization();
        }

        public List<VerificationCodes> GetAll()
        {
            return _vertificationCodeDal.GetAll();
        }

        public VerificationCodes GetById(int id)
        {
            return _vertificationCodeDal.GetById(id);
        }

        public ResponseDto<List<CheckinTimeResponse>> GetCheckinTimeByDate(int hotelid, DateTime startDate, DateTime endDate, TokenKeys keys)
        {
            var data = _vertificationCodeDal.GetCheckinTimeByDate(hotelid, startDate, endDate);
            var hotel = _hotelService.GetById(keys, hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.data.Companyid) || keys.role == 1)
            {
                return new ResponseDto<List<CheckinTimeResponse>>().Success(data, 200);
            }
            else if (keys.role == 3)
            {
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                return new ResponseDto<List<CheckinTimeResponse>>().Success(data, 200);

            }
            return new ResponseDto<List<CheckinTimeResponse>>().Authorization();
        }

        public ResponseDto<GetRoomOccupancyRateByDateResponse> GetRoomOccupancyRateByDate(int hotelid, DateTime startDate, TokenKeys keys)
        {
            var hotel = _hotelService.GetById(keys, hotelid);
            var value = _vertificationCodeDal.GetRoomOccupancyRateByDate(hotelid, startDate);
            if (keys.role == 2 && (keys.companyid == hotel.data.Companyid) || keys.role == 1)
            {
                return new ResponseDto<GetRoomOccupancyRateByDateResponse>().Success(value, 200);
            }
            else if (keys.role == 3)
            {
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                return new ResponseDto<GetRoomOccupancyRateByDateResponse>().Success(value, 200);
            }
            return new ResponseDto<GetRoomOccupancyRateByDateResponse>().Authorization();
        }

        public ResponseDto<VerificationCodeResponse> GetVertificationProduce(VerificationCodeRequest request, TokenKeys keys)
        {
            var hotel = _hotelService.GetById(keys, request.hotelid);

            if (hotel.data.Companyid == keys.companyid)
            {
                var data = _vertificationCodeDal.GetVertificationProduce(request);
                return data;
            }
            return new ResponseDto<VerificationCodeResponse>().Authorization();
        }

        public ResponseDto<VerificationCodeResponse> Update(VerificationCodeRequest entity, TokenKeys keys)
        {
            throw new NotImplementedException();
        }


        ResponseDto<RoomCountResponse> IVerificationCodeService.GetRoomCountByDate(int hotelid, DateTime date, TokenKeys keys)
        {
            var hotel = _hotelService.GetById(keys, hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.data.Companyid) || keys.role == 1)
            {
                var GeneralHotelPoint = _vertificationCodeDal.GetRoomCountByDate(hotelid, date);
                return new ResponseDto<RoomCountResponse>().Success(GeneralHotelPoint, 200);
            }
            if (keys.role == 3)
            {
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                var GeneralHotelPoint = _vertificationCodeDal.GetRoomCountByDate(hotelid, date);
                return new ResponseDto<RoomCountResponse>().Success(GeneralHotelPoint, 200);
            }
            return new ResponseDto<RoomCountResponse>().Authorization();
        }

        ResponseDto<RoomCountResponse> IVerificationCodeService.GetTotalRoomCountOfUsedApp(int hotelid, TokenKeys keys)
        {
            var hotel = _hotelService.GetById(keys, hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.data.Companyid) || keys.role == 1)
            {
                var GetTotalRoomCountOfUsedAppCount = _vertificationCodeDal.GetTotalRoomCountOfUsedApp(hotelid);
                return new ResponseDto<RoomCountResponse>().Success(GetTotalRoomCountOfUsedAppCount, 200);
            }
            if (keys.role == 3)
            {
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                var GetTotalRoomCountOfUsedAppCount = _vertificationCodeDal.GetTotalRoomCountOfUsedApp(hotelid);
                return new ResponseDto<RoomCountResponse>().Success(GetTotalRoomCountOfUsedAppCount, 200);
            }
            return new ResponseDto<RoomCountResponse>().Authorization();
        }

        ResponseDto<RoomCountResponse> IVerificationCodeService.GetTotalRoomCountOfUsedAppDateSearch(int hotelid, DateTime starDate, DateTime endDate, TokenKeys keys)
        {
            var hotel = _hotelService.GetById(keys, hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.data.Companyid) || keys.role == 1)
            {
                var GetTotalRoomCountOfUsedAppCount = _vertificationCodeDal.GetTotalRoomCountOfUsedAppDateSearch(hotelid, starDate, endDate);
                return new ResponseDto<RoomCountResponse>().Success(GetTotalRoomCountOfUsedAppCount, 200);
            }
            if (keys.role == 3)
            {
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(keys.userId, hotelid);
                var GetTotalRoomCountOfUsedAppCount = _vertificationCodeDal.GetTotalRoomCountOfUsedAppDateSearch(hotelid, starDate, endDate);
                return new ResponseDto<RoomCountResponse>().Success(GetTotalRoomCountOfUsedAppCount, 200);
            }
            return new ResponseDto<RoomCountResponse>().Authorization();
        }
    }
}
