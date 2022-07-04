using System.Collections.Generic;
using Worigo.Entity.Abstrack;
namespace Worigo.Entity.Concrete
{
    public class Services:IBaseEntity
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int hotelid { get; set; }
 
        public List<GeneralServiceAndService> GeneralServiceAndServices{ get; set; }
    }
}
