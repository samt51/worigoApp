using AutoMapper;
using System;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.Reports.Response;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.VertificationCodeDto.Request;
using Worigo.Core.Dtos.VertificationCodeDto.Response;
using Worigo.Core.Extension;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class VertificationCodeManager : IVertificationCodeService
    {
        private readonly IVertificationCodeDal _vertificationCodeDal;
        private readonly IHotelService _hotelService;
        private readonly IManagementOfHotelsDal _managementOfHotelsDal;
        private readonly IMapper _mapper;
        public VertificationCodeManager(IVertificationCodeDal vertificationCodeDal, IHotelService hotelService, IMapper mapper, IManagementOfHotelsDal managementOfHotelsDal)
        {
            _vertificationCodeDal = vertificationCodeDal;
            _mapper = mapper;
            _hotelService = hotelService;
            _managementOfHotelsDal = managementOfHotelsDal;
        }

        public ResponseDto<VertificationCodeResponse> Create(VertificationCodeRequest entity, TokenKeys keys)
        {

            if (keys.companyid == _hotelService.GetById(keys, entity.hotelid).Companyid || keys.role == 1)
            {
                entity.Code = CodeRandomGeneration.RandomVertificationCodeCreate(4).ToString();
                var response = _vertificationCodeDal.Create(_mapper.Map<VertificationCodes>(entity));
                var mapResponse = _mapper.Map<VertificationCodeResponse>(response);
                return new ResponseDto<VertificationCodeResponse>().Success(mapResponse, 200);
            }
            return new ResponseDto<VertificationCodeResponse>().Authorization();
        }

        public List<VertificationCodes> GetAll()
        {
            return _vertificationCodeDal.GetAll();
        }

        public VertificationCodes GetById(int id)
        {
            return _vertificationCodeDal.GetById(id);
        }



        public ResponseDto<GetRoomOccupancyRateByDateResponse> GetRoomOccupancyRateByDate(int hotelid, DateTime startDate, TokenKeys keys)
        {
            var hotel = _hotelService.GetById(keys, hotelid);
            var value = _vertificationCodeDal.GetRoomOccupancyRateByDate(hotelid, startDate);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
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





        public ResponseDto<VertificationCodeResponse> Update(VertificationCodeRequest entity, TokenKeys keys)
        {
            throw new NotImplementedException();
        }


        ResponseDto<RoomCountResponse> IVertificationCodeService.GetRoomCountByDate(int hotelid, DateTime date, TokenKeys keys)
        {
            var hotel = _hotelService.GetById(keys, hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
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

        ResponseDto<RoomCountResponse> IVertificationCodeService.GetTotalRoomCountOfUsedApp(int hotelid, TokenKeys keys)
        {
            var hotel = _hotelService.GetById(keys, hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
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

        ResponseDto<RoomCountResponse> IVertificationCodeService.GetTotalRoomCountOfUsedAppDateSearch(int hotelid, DateTime starDate, DateTime endDate, TokenKeys keys)
        {
            var hotel = _hotelService.GetById(keys, hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
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
