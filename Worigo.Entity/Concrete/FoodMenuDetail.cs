using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class FoodMenuDetail:IBaseEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public int foodMenuId { get; set; }

    }
}
