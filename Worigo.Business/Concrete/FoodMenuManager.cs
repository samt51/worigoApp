using AutoMapper;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.FoodMenu.Request;
using Worigo.Core.Dtos.FoodMenu.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class FoodMenuManager : IFoodMenuService
    {
        private readonly IFoodMenuDal _foodMenuDal;
        private readonly IMapper _mapper;
        private readonly IHotelDal _hotelDal;
        private readonly IDirectorsDepartmansDal _directorsDepartmansDal;
        private readonly IManagementOfHotelsDal _managementOfHotelsDal;
        public FoodMenuManager(IFoodMenuDal foodMenuDal, IMapper mapper, IHotelDal hotelDal, IDirectorsDepartmansDal directorsDepartmansDal, IManagementOfHotelsDal managementOfHotelsDal)
        {
            _foodMenuDal = foodMenuDal;
            _mapper = mapper;
            _hotelDal = hotelDal;
            _directorsDepartmansDal = directorsDepartmansDal;
            _managementOfHotelsDal = managementOfHotelsDal;
        }



        public ResponseDto<FoodMenuResponse> Create(FoodMenuRequest entity, TokenKeys keys)
        {
            var hotel = _hotelDal.GetById(entity.hotelid);

            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
            {
                var response = _foodMenuDal.Create(_mapper.Map<FoodMenu>(entity));
                return new ResponseDto<FoodMenuResponse>().Success(_mapper.Map<FoodMenuResponse>(response), 200);
            }
            else if (keys.role == 3 || keys.role == 5)
            {
                if (keys.role == 5)
                {
                    _directorsDepartmansDal.GetDirectoryByHotelIdAndId(entity.hotelid, keys.userId);
                    var response1 = _foodMenuDal.Create(_mapper.Map<FoodMenu>(entity));
                    return new ResponseDto<FoodMenuResponse>().Success(_mapper.Map<FoodMenuResponse>(response1), 200);
                }
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(keys.userId, entity.hotelid);
                var response = _foodMenuDal.Create(_mapper.Map<FoodMenu>(entity));
                return new ResponseDto<FoodMenuResponse>().Success(_mapper.Map<FoodMenuResponse>(response), 200);
            }
            return new ResponseDto<FoodMenuResponse>().Authorization();
        }



        public ResponseDto<FoodMenuResponse> GetById(int id, TokenKeys keys)
        {
            var menuData = _foodMenuDal.GetById(id);
            var hotel = _hotelDal.GetById(menuData.hotelid);
            var menuDto = _mapper.Map<FoodMenuResponse>(menuData);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
            {
                return new ResponseDto<FoodMenuResponse>().Success(menuDto, 200);
            }
            else if (keys.role == 3)
            {
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(keys.userId, menuData.hotelid);
                return new ResponseDto<FoodMenuResponse>().Success(menuDto, 200);
            }
            else if (keys.role == 5)
            {
                _directorsDepartmansDal.GetDirectoryByHotelIdAndId(menuData.hotelid, keys.userId);
                return new ResponseDto<FoodMenuResponse>().Success(menuDto, 200);
            }
            else
                return new ResponseDto<FoodMenuResponse>().Authorization();
        }


        public ResponseDto<List<FoodMenuResponse>> GetMenuByHotelId(int hotelId, TokenKeys keys)
        {
            var hotel = _hotelDal.GetById(hotelId);
            var responselist = _foodMenuDal.GetMenuByHotelId(hotelId);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
            {
                return new ResponseDto<List<FoodMenuResponse>>().Success(responselist, 200);
            }
            else if (keys.role == 3 || keys.role == 5)
            {
                if (keys.role == 5)
                {
                    _directorsDepartmansDal.GetDirectoryByHotelIdAndId(hotelId, keys.userId);
                    return new ResponseDto<List<FoodMenuResponse>>().Success(responselist, 200);

                }
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(keys.userId, hotelId);
                return new ResponseDto<List<FoodMenuResponse>>().Success(responselist, 200);
            }
            return new ResponseDto<List<FoodMenuResponse>>().Authorization();
        }



        public ResponseDto<FoodMenuResponse> Update(FoodMenuRequest entity, TokenKeys keys)
        {
            var hotel = _hotelDal.GetById(entity.hotelid);
            var mapdto = _mapper.Map<FoodMenu>(entity);
            if (keys.role == 2 && (keys.companyid == hotel.Companyid) || keys.role == 1)
            {
                var response = _foodMenuDal.Update(mapdto);
                return new ResponseDto<FoodMenuResponse>().Success(_mapper.Map<FoodMenuResponse>(response), 200);
            }
            else if (keys.role == 3)
            {
                _managementOfHotelsDal.GetManagementBymanagementIdByHotelid(keys.userId, entity.hotelid);
                var response = _foodMenuDal.Update(mapdto);
                return new ResponseDto<FoodMenuResponse>().Success(_mapper.Map<FoodMenuResponse>(response), 200);
            }
            else if (keys.role == 5)
            {
                _directorsDepartmansDal.GetDirectoryByHotelIdAndId(entity.hotelid, keys.userId);
                var response = _foodMenuDal.Update(mapdto);
                return new ResponseDto<FoodMenuResponse>().Success(_mapper.Map<FoodMenuResponse>(response), 200);
            }
            else
                return new ResponseDto<FoodMenuResponse>().Authorization();
        }
    }
}
