using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class HotelOfService : IBaseEntity
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int HotelId { get; set; }
    }
}
