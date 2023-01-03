using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class ManagementOfHotels:IBaseEntity
    {
        public int id { get; set; }
        public int hotelid { get; set; }
        public int managementid { get; set; }
    }
}
