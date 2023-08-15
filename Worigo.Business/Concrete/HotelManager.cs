using AutoMapper;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.Hotel.Request;
using Worigo.Core.Dtos.Hotel.Response;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Exceptions;
using Worigo.Core.Extension;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class HotelManager : IHotelService
    {
        private readonly IHotelDal _hotelsDal;
        private readonly IManagementOfHotelService _managementOfHotelsDal;
        private readonly ICommentService _companiesDal;
        private readonly IMapper _mapper;
        private readonly ICompaniesService _companiesService;
        private readonly IServicesDal _servicesDal;
        private readonly IHotelOfServiceDal _hotelOfServiceDal;
        public HotelManager(IHotelOfServiceDal hotelOfServiceDal, IServicesDal servicesDal, ICompaniesService companiesService, IMapper mapper, IManagementOfHotelService managementOfHotelsDal, IHotelDal hotelsDal)
        {
            _hotelsDal = hotelsDal;
            _companiesService = companiesService;
            _managementOfHotelsDal = managementOfHotelsDal;
            _mapper = mapper;
            _hotelOfServiceDal = hotelOfServiceDal;
            _servicesDal = servicesDal;
        }


        public ResponseDto<HotelResponse> Create(TokenKeys data, HotelAddOrUpdateRequest entity)
        {
            _companiesDal.GetById(entity.Companyid, data);
            if ((data.companyid == entity.Companyid) && data.role == 2)
            {
                if (entity.file != null)
                {
                    entity.ImageUrl = FileToByteConvert.FromFileToByte(entity.file);
                }
                var map = _hotelsDal.Create(_mapper.Map<Hotel>(entity));
                var services = _servicesDal.GetAll(x=>x.isDeleted==false&&x.isActive==true);
                foreach (var item in services)
                {
                    var saveEntity = new HotelOfService
                    {
                        ServiceId=item.id,
                        HotelId=map.id,
                        CreatedDate=System.DateTime.Now,
                        isActive=true,
                        isDeleted=false,
                        ModifyDate=System.DateTime.Now
                    };
                    _hotelOfServiceDal.Create(saveEntity);
                }
                return new ResponseDto<HotelResponse>().Success(_mapper.Map<HotelResponse>(map), 200);
            }
            else if (data.role == 1)
            {
                var map = _hotelsDal.Create(_mapper.Map<Hotel>(entity));
                var services = _servicesDal.GetAll(x => x.isDeleted == false && x.isActive == true);
                foreach (var item in services)
                {
                    var saveEntity = new HotelOfService
                    {
                        ServiceId = item.id,
                        HotelId = map.id,
                        CreatedDate = System.DateTime.Now,
                        isActive = true,
                        isDeleted = false,
                        ModifyDate = System.DateTime.Now
                    };
                    _hotelOfServiceDal.Create(saveEntity);
                }
                return new ResponseDto<HotelResponse>().Success(_mapper.Map<HotelResponse>(map), 200);
            }
            return new ResponseDto<HotelResponse>().Authorization();
        }

        public ResponseDto<List<HotelResponse>> GetAll(TokenKeys keys)
        {
            var data = _mapper.Map<List<HotelResponse>>(_hotelsDal.GetAll(x => x.isDeleted == false));
            return new ResponseDto<List<HotelResponse>>().Success(data, 200);
        }
        public ResponseDto<List<HotelResponse>> GetHotelByCompanyid(int companyid, TokenKeys keys)
        {
            _companiesService.GetById(companyid, keys);
            if ((keys.companyid == companyid) && keys.role == 2 || keys.role == 1)
            {
                var listdata = _hotelsDal.GetHotelByCompanyid(companyid);
                return new ResponseDto<List<HotelResponse>>().Success(listdata, 200);
            }
            return new ResponseDto<List<HotelResponse>>().Authorization();
        }

        public ResponseDto<HotelResponse> GetHotelByCompanyIdAndHotelId(int companyid, int hotelid, TokenKeys keys)
        {
            _companiesService.GetById(companyid, keys);
            if ((keys.companyid == companyid) && keys.role == 2 || keys.role == 1)
            {
                var listdata = _hotelsDal.GetHotelByCompanyIdAndHotelId(companyid, hotelid);
                return new ResponseDto<HotelResponse>().Success(listdata, 200);
            }
            return new ResponseDto<HotelResponse>().Authorization();
        }

        public ResponseDto<HotelResponse> Update(HotelAddOrUpdateRequest entity, TokenKeys keys)
        {
            var singularHotel = _hotelsDal.GetById(entity.id);
            if (entity.file != null)
            {
                entity.ImageUrl = FileToByteConvert.FromFileToByte(entity.file);
            }
            singularHotel.HotelName = entity.HotelName;
            singularHotel.Adress = entity.Adress;
            singularHotel.PhoneNumber = entity.PhoneNumber;
            singularHotel.Email = entity.Email;
            singularHotel.NumberOfStar = entity.NumberOfStar;
            singularHotel.ModifyDate = System.DateTime.Now;
            var data = _hotelsDal.Update(singularHotel);

            return new ResponseDto<HotelResponse>().Success(_mapper.Map<HotelResponse>(data), 200);
        }

        ResponseDto<HotelResponse> IHotelService.GetById(TokenKeys token, int id)
        {
            var data = _hotelsDal.GetById(id);
            var mapper = _mapper.Map<HotelResponse>(data);
            return new ResponseDto<HotelResponse>().Success(mapper, 200);
        }
    }
}
