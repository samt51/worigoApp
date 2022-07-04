using System;
using Worigo.Entity.Concrete;

namespace Worigo.Core.Dtos.ListDto
{
    public class VertificationCodeDto: BaseEntity
    {
        public int id { get; set; }
        public int hotelid { get; set; }
        public int roomid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Code { get; set; }
        public bool isActive { get; set; }
    }
}
