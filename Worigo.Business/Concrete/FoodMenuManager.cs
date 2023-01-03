using AutoMapper;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.FoodMenu.Response;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class FoodMenuManager : IFoodMenuService
    {
        private readonly IFoodMenuDal _foodMenuDal;
        private readonly IMapper _mapper;
        public FoodMenuManager(IFoodMenuDal foodMenuDal,IMapper mapper)
        {
            _foodMenuDal = foodMenuDal;
            _mapper = mapper;   
        }
    
        public FoodMenu Create(FoodMenu entity)
        {
        
          return _foodMenuDal.Create(entity);
        }

      
        public FoodMenu GetById(int id)
        {
            return _foodMenuDal.GetById(id);
        }

        public List<NewMenuListResponse> GetMenuByHotelId(int hotelId)
        {
            return _foodMenuDal.GetMenuByHotelId(hotelId);
        }

        public FoodMenu Update(FoodMenu entity)
        {
           return _foodMenuDal.Update(entity);
        }
    }
}
