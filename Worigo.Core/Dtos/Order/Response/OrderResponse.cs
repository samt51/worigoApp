using System;

namespace Worigo.Core.Dtos.Order.Response
{
    public class OrderResponse
    {
        public int id { get; set; }
        public int serviceId { get; set; }
        public string Service { get; set; }
        public int serviceValueId { get; set; }
        public string ServiceValue { get; set; }
        public int Quantity { get; set; }
        public decimal unitPrice { get; set; }
        public decimal totalPrice { get; set; }
        public int customerId { get; set; }
        public string CustomerFullName { get; set; }
        public string RoomNo { get; set; }
        public int hotelid { get; set; }
        public string HotelName { get; set; }
        public DateTime orderDate { get; set; } = DateTime.Now;
        public int? EmployeeId { get; set; }
        public string EmployeeFullName { get; set; }
        public string EmployeePositionName { get; set; }
        public string EmployeeImageUrl { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string CancelDescription { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }

    }
}
