using System;
using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class TasksOfEmployees:IBaseEntity
    {
        public int id { get; set; }
        public int hotelid { get; set; }
        public int employeeid { get; set; }
        public int serviceValue { get; set; }
        public DateTime starDate { get; set; }
        public DateTime? finishDate{ get; set; }
        public int roomNumber { get; set; }
        public int isCompleted { get; set; }
    }
}
