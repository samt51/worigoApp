using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.ContentsOfMenuDetail.Response;
using Worigo.Core.Dtos.FoodMenuDetailDto.Response;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfFoodMenuDetailDal : EfRepositoryDal<FoodMenuDetail, DataContext>, IFoodMenuDetailDal
    {
        static IMapper _mapper;
        public List<FoodMenuDetailResponse> GetAllByMenuId(int menuid)
        {
            using (var db = new DataContext())
            {
                var joinlist = from d1 in db.FoodMenuDetail.Where(x => x.foodMenuId == menuid && x.isDeleted == false && x.isActive == true)
                               join d3 in db.FoodMenu on menuid equals d3.id
                               select new FoodMenuDetailResponse
                               {
                                   id = d1.id,
                                   Description = d1.description,
                                   FoodMenuId = d1.foodMenuId,
                                   MenuName = d3.categoryName,
                                   Name = d1.name,
                                   Price = d1.price,
                               };
                joinlist.ToList().ForEach(x =>
                {
                    var contents = db.ContentsOfFood.Where(x => x.foodMenuDetailId == x.foodMenuDetailId && x.isActive == true && x.isDeleted == false).ToList();
                    x.ContentOfMenu = _mapper.Map<List<ContentsOfMenuResponse>> (contents);
                });
                return joinlist.ToList();
            }
        }

        public FoodMenuDetailResponse GetByDetailId(int menuDetailid)
        {
            GetById(menuDetailid);
            using (var db = new DataContext())
            {
                var join = from d1 in db.FoodMenuDetail.Where(x => x.isActive == true && x.isDeleted == false && x.id == menuDetailid)
                           join d2 in db.FoodMenu.Where(x => x.isActive == true && x.isDeleted == false) on d1.foodMenuId equals d2.id
                           select new FoodMenuDetailResponse
                           {
                               id = d1.id,
                               Description = d1.description,
                               FoodMenuId = d1.foodMenuId,
                               MenuName = d2.categoryName,
                               Name = d1.name,
                               Price = d1.price
                           };
                var contents = db.ContentsOfFood.Where(x => x.foodMenuDetailId == menuDetailid).ToList();
                join.First().ContentOfMenu = _mapper.Map<List<ContentsOfMenuResponse>>(contents);
                return join.First();
            }
        }
    }
}
