namespace Worigo.Core.Dtos.HotelOfServiceDto.Request
{
    public class HotelOfServiceAddOrUpdate
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string Service { get; set; }
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public bool IsActive { get; set; }
    }
}
