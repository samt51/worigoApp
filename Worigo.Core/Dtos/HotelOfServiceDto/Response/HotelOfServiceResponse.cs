namespace Worigo.Core.Dtos.HotelOfServiceDto.Response
{
    public class HotelOfServiceResponse
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string Service { get; set; }
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public bool IsActive { get; set; }
    }
}
