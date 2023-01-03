using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class ContentsOfFood:IBaseEntity
    {
        public int id { get; set; }
        public int name { get; set; }
        public int foodMenuDetailId { get; set; }
    }
}
