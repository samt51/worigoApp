using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class Room : IBaseEntity
    {
        public int id { get; set; }
        public int RoomTypeid { get; set; }
        public int NumberOfBeds { get; set; }
        public int RoomNo { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int hotelid { get; set; }
        public bool IsFull { get; set; } = false;
    }
}
