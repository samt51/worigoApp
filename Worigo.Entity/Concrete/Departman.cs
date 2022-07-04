using System.Collections.Generic;
using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class Departman:IBaseEntity
    {
        public int Id { get; set; }
        public string DepartmanName { get; set; }
        public int Hotelid { get; set; }
    }
}