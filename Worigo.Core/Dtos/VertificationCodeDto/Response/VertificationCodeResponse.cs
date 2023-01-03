using System;

namespace Worigo.Core.Dtos.VertificationCodeDto.Response
{
    public class VertificationCodeResponse
    {
        public int id { get; set; }
        public int roomid { get; set; }
        public int hotelid { get; set; }
        public string hotelName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Code { get; set; }
    }
}
