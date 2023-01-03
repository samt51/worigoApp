using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class FoodMenu:IBaseEntity
    {
        public int id { get; set; }
        public string categoryName { get; set; }
        public int hotelid { get; set; }

    }
}
