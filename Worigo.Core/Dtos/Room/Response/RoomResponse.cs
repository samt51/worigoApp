using System;

namespace Worigo.Core.Dtos.Room.Response
{
    public class RoomResponse
    {
        public int roomId { get; set; }
        public int hotelId { get; set; }
        public string hotelName { get; set; }
        public string roomNo { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int? daily{ get; set; }
    }
}
