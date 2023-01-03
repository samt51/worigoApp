using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class ServicesValues:IBaseEntity
    {
        public int id { get; set; }
        public string value { get; set; }
        public int Serviceid { get; set; }
        public string ImageUrl { get; set; }

    }
}
