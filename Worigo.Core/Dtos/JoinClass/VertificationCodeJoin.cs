using System;

namespace Worigo.Core.Dtos.JoinClass
{
    public class VertificationCodeJoin
    {
        public int id { get; set; }
        public int roomno { get; set; }
        public string hotelname{ get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Code { get; set; }
    }
}
