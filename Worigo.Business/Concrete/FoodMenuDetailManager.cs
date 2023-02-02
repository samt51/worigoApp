using AutoMapper;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.FoodMenuDetailDto.Dto;
using Worigo.Core.Dtos.FoodMenuDetailDto.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class FoodMenuDetailManager : IFoodMenuDetailService
    {
        private readonly IFoodMenuDetailDal _foodMenuDetailDal;
        private readonly IContentsOfFoodDal _contentsOfFoodDal;
        private readonly IFoodMenuDal _foodMenuDal;
        private readonly IMapper _mapper;
        private readonly IHotelDal _hotelDal;
        private readonly IManagementOfHotelsDal _managementOfHotelsDal;
        private readonly IDirectorsDepartmansDal _directorsDepartmansDal;
        public FoodMenuDetailManager(IFoodMenuDetailDal foodMenuDetailDal, IContentsOfFoodDal contentsOfFoodDal, IMapper mapper, IHotelDal hotelDal, IFoodMenuDal foodMenuDal, IManagementOfHotelsDal managementOfHotelsDal, IDirectorsDepartmansDal directorsDepartmansDal)
        {
            _foodMenuDetailDal = foodMenuDetailDal;
            _contentsOfFoodDal = contentsOfFoodDal;
            _foodMenuDal = foodMenuDal;
            _mapper = mapper;
            _hotelDal = hotelDal;
            _managementOfHotelsDal = managementOfHotelsDal;
            _directorsDepartmansDal = directorsDepartmansDal;
        }

        public ResponseDto<FoodMenuDetailResponse> Create(FoodMenuDetailDtoAddOrUpdateRequest entity, TokenKeys keys)
        {
            var foodMenu = _foodMenuDal.GetById(entity.foodMenuId);
            var hotel = _hotelDal.GetById(foodMenu.hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
            {
                var response = _foodMenuDetailDal.Create(_mapper.Map<FoodMenuDetail>(entity));
                return new ResponseDto<FoodMenuDetailResponse>().Success(_mapper.Map<FoodMenuDetailResponse>(response), 200);
            }
            else if (keys.role == 3)
            {
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(keys.userId, hotel.id);
                var response = _foodMenuDetailDal.Create(_mapper.Map<FoodMenuDetail>(entity));
                return new ResponseDto<FoodMenuDetailResponse>().Success(_mapper.Map<FoodMenuDetailResponse>(response), 200);
            }
            else if (keys.role == 5)
            {
                _directorsDepartmansDal.GetDirectoryByHotelIdAndId(hotel.id, keys.userId);
                var response = _foodMenuDetailDal.Create(_mapper.Map<FoodMenuDetail>(entity));
                return new ResponseDto<FoodMenuDetailResponse>().Success(_mapper.Map<FoodMenuDetailResponse>(response), 200);
            }
            else
                return new ResponseDto<FoodMenuDetailResponse>().Authorization();
        }


        public ResponseDto<List<FoodMenuDetailResponse>> GetAllByMenuId(int menuid, TokenKeys keys)
        {
            var menu = _foodMenuDal.GetById(menuid);
            var hotel = _hotelDal.GetById(menu.hotelid);
            if (keys.role >= 1 && keys.role <= 5 && (keys.companyid == hotel.Companyid))
            {
                var foodmenudetail = _foodMenuDetailDal.GetAllByMenuId(menuid);
                return new ResponseDto<List<FoodMenuDetailResponse>>().Success(foodmenudetail, 200);
            }
            return new ResponseDto<List<FoodMenuDetailResponse>>().Authorization();
        }



        public ResponseDto<FoodMenuDetailResponse> GetByDetailId(int menuDetailid, TokenKeys keys)
        {
            var data = _foodMenuDetailDal.GetById(menuDetailid);
            return new ResponseDto<FoodMenuDetailResponse>().Success(_mapper.Map<FoodMenuDetailResponse>(data), 200);
        }


        public ResponseDto<FoodMenuDetailResponse> Update(FoodMenuDetailDtoAddOrUpdateRequest entity, TokenKeys keys)
        {
            var foodMenu = _foodMenuDal.GetById(entity.foodMenuId);
            var hotel = _hotelDal.GetById(foodMenu.hotelid);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
            {
                var response = _foodMenuDetailDal.Update(_mapper.Map<FoodMenuDetail>(entity));
                return new ResponseDto<FoodMenuDetailResponse>().Success(_mapper.Map<FoodMenuDetailResponse>(response), 200);

            }
            else if (keys.role == 3)
            {
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(keys.userId, hotel.id);
                var response = _foodMenuDetailDal.Update(_mapper.Map<FoodMenuDetail>(entity));
                return new ResponseDto<FoodMenuDetailResponse>().Success(_mapper.Map<FoodMenuDetailResponse>(response), 200);
            }
            else if (keys.role == 5)
            {
                _directorsDepartmansDal.GetDirectoryByHotelIdAndId(hotel.id, keys.userId);
                var response = _foodMenuDetailDal.Update(_mapper.Map<FoodMenuDetail>(entity));
                return new ResponseDto<FoodMenuDetailResponse>().Success(_mapper.Map<FoodMenuDetailResponse>(response), 200);
            }
            else
                return new ResponseDto<FoodMenuDetailResponse>().Authorization();
        }
    }
}
