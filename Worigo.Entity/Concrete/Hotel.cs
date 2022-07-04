using Worigo.Entity.Abstrack;
namespace Worigo.Entity.Concrete
{
    public class Hotel : IBaseEntity
    {
        public int id { get; set; }
        public string HotelName { get; set; }
        public string ImageUrl { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int NumberOfStar { get; set; }
    }
}
