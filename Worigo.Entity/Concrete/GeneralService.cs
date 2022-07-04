using System.Collections.Generic;
using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class GeneralService:IBaseEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public string  ImageUrl { get; set; }
        public List<GeneralServiceAndService> GeneralServiceAndServices{ get; set; }
    }
}
