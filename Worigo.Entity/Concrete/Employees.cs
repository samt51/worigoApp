using System;
using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class Employees:IBaseEntity
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImageUrl { get; set; }
        public int FloorNo { get; set; }
        public string phoneNumber { get; set; }
        public bool gender { get; set; }
        public DateTime StartDateOfWork { get; set; }
        public DateTime ExitEntryDate { get; set; }
        public int userid { get; set; }
        public int hotelid { get; set; }
        public int departmanid { get; set; }
    }
}
