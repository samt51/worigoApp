using Worigo.Entity.Concrete;

namespace Worigo.Core.Dtos.ListDto
{
    public class RoomDto: BaseEntity
    {
        public int id { get; set; }
        public int RoomTypeid { get; set; }
        public RoomType RoomType { get; set; }
        public int NumberOfBeds { get; set; }
        public int RoomNo { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int hotelid { get; set; }
        public bool isActive { get; set; }
    }
}
