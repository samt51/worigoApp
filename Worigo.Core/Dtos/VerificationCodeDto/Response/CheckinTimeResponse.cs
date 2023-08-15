using System;

namespace Worigo.Core.Dtos.VerificationCodeDto.Response
{
    public class CheckinTimeResponse
    {
        public DateTime startDate{ get; set; }
        public DateTime endDate{ get; set; }
        public int hotelId { get; set; }
        public string hotelName  { get; set; }
        public int roomId { get; set; }
        public string room { get; set; }
    }
}
