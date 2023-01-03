using System;
using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class WaitingOrders:IBaseEntity
    {
        public int id { get; set; }
        public int orderid { get; set; }
        public DateTime isProccessingDate{ get; set; }
    }
}
