using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class ServiceOfValues : IBaseEntity
    {
        public int id { get; set; }
        public int ServiceId { get; set; }
        public int ServiceValueId { get; set; }
        public int HotelId { get; set; }
    }
}
