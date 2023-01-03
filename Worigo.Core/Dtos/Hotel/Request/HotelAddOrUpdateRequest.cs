using Microsoft.AspNetCore.Http;

namespace Worigo.Core.Dtos.Hotel.Request
{
    public class HotelAddOrUpdateRequest
    {
        public int id { get; set; }
        public string HotelName { get; set; }
        public string ImageUrl { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int NumberOfStar { get; set; }
        public int Companyid { get; set; }
        public IFormFile?  file{ get; set; }
 
    }
}
