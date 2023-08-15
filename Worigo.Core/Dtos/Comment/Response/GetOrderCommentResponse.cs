using System;

namespace Worigo.Core.Dtos.Comment.Response
{
    public class GetOrderCommentResponse
    {
        public int Id { get; set; }
        public int EmployeePoint { get; set; }
        public int speedPoint { get; set; }
        public int contentsPoint { get; set; }
        public int hotelid { get; set; }
        public string HotelName { get; set; }
        public int RoomId { get; set; }
        public string RoomNo { get; set; }
        public string Commentary { get; set; }
        public int OrderId { get; set; }
        public int ServiceId { get; set; }
        public string ServiceValue { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate{ get; set; }
        public DateTime? CompletedDate { get; set; }
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public double GeneralPoint { get; set; }
    }
}
