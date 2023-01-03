using System;
using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class Order:IBaseEntity
    {
        public int id { get; set; }
        public int serviceValueId { get; set; }
        public int Quantity { get; set; }
        public decimal unitPrice { get; set; }
        public decimal totalPrice { get; set; }
        public int customerId { get; set; }
        public int hotelid { get; set; }
        public DateTime orderDate { get; set; }=DateTime.Now;   
    }
}
