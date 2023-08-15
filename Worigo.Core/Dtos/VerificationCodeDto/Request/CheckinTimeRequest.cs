using System;

namespace Worigo.Core.Dtos.VerificationCodeDto.Request
{
   public class CheckinTimeRequest
    {
        public DateTime startDate{ get; set; }
        public DateTime endDate { get; set; }
        public int hotelid { get; set; }
    }
}
