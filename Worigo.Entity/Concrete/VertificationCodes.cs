using System;
using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
  public class VertificationCodes:IBaseEntity
    {
        public int id { get; set; }
        public int roomid { get; set; }
        public int hotelid { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate{ get; set; }
        public DateTime FinishDate { get; set; }
        public string Code { get; set; }
        public bool IsFull { get; set; }
    }
}
