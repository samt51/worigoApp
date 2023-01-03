using System;

namespace Worigo.Core.Dtos.JoinClass
{
    public class RoomListJoin
    {
        public int id { get; set; }
        public int RoomTypeid { get; set; }
        public string RoomType { get; set; }
        public int NumberOfBeds { get; set; }
        public int RoomNo { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int hotelid { get; set; }
        public bool IsFull { get; set; }
        public string Hotel{ get; set; }
        public string Company { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int? daily { get; set; }
    }
}
