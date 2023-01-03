using System;
using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class EmployeeOfOrder:IBaseEntity
    {
        public int id { get; set; }
        public int orderid { get; set; }
        public int employeeid { get; set; }
        public DateTime isProccessingDate { get; set; }
        public int status { get; set; }
    }
}
