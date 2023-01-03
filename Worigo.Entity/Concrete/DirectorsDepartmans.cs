using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class DirectorsDepartmans:IBaseEntity
    {
        public int id { get; set; }
        public int hotelid { get; set; }
        public int directoryid { get; set; }
        public int departmanid { get; set; }
    }
}
