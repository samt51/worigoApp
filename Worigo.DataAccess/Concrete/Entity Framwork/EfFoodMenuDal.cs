using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.FoodMenu.Response;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfFoodMenuDal : EfRepositoryDal<FoodMenu, DataContext>, IFoodMenuDal
    {
        public List<FoodMenuResponse> GetMenuByHotelId(int hotelId)
        {
            using (var db = new DataContext())
            {
                var menulist = db.FoodMenu.Where(x => x.hotelid == hotelId && x.isDeleted == false).ToList();
                var hotel = db.Hotel.ToList();
                var join = from d1 in menulist
                           join d2 in hotel on d1.hotelid equals d2.id
                           select new FoodMenuResponse
                           {
                               id = d1.id,
                               hotel = d2.HotelName,
                               menuName = d1.categoryName
                           };
                return join.ToList();
            }
        }
    }
}
