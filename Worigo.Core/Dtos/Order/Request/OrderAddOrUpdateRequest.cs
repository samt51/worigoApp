using System;

namespace Worigo.Core.Dtos.Order.Request
{
    public class OrderAddOrUpdateRequest
    {
        public int id { get; set; }
        public int serviceValueId { get; set; }
        public int Quantity { get; set; }
        public decimal unitPrice { get; set; }
        public decimal totalPrice { get; set; }
        public DateTime orderDate { get; set; } = DateTime.Now;
        public int? EmployeeId { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int VertificationId { get; set; }
        public int HotelId { get; set; }
    }
}
