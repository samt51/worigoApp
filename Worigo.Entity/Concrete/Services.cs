using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class Services:IBaseEntity
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int HotelId { get; set; }
    }
}
