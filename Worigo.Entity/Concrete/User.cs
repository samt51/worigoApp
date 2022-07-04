using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class User : IBaseEntity
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int roleid { get; set; }
        public int hotelid{ get; set; }
    }
}
