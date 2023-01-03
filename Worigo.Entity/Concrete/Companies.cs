using System.Collections.Generic;
using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class Companies: IBaseEntity
    {
        public int id { get; set; }
        public string name{ get; set; }
        public List<Hotel> Hotels{ get; set; }

    }
}
