namespace Worigo.Core.Dtos.ServiceOfValueDto.Response
{
    public class ServiceOfValueResponse
    {
        public int id { get; set; }
        public int ServiceId { get; set; }
        public string Service { get; set; }
        public int ServiceValueId { get; set; }
        public string ServiceValue { get; set; }
        public int HotelId { get; set; }
        public string HotelName { get; set; }
    }
}
