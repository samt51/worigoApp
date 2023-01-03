using System;

namespace Worigo.Core.Dtos.Order.Request
{
    public class OrderRequestDto
    {
        public int serviceValueId { get; set; }
        public int Quantity { get; set; }
        public decimal unitPrice { get; set; }
        public decimal totalPrice { get; set; }
        public int customerId { get; set; }
        public int hotelid { get; set; }

        public DateTime orderDate { get; set; } = DateTime.Now;
    }
}
