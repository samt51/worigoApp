namespace Worigo.Core.Dtos.ServiceOfValueDto.Request
{
    public class ServiceOfValueAddOrUpdate
    {
        public int id { get; set; }
        public int ServiceId { get; set; }
        public int ServiceValueId { get; set; }
        public int HotelId { get; set; }
    }
}
