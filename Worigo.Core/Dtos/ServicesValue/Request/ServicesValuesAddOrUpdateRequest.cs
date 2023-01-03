using Microsoft.AspNetCore.Http;

namespace Worigo.Core.Dtos.ServicesValue.Request
{
    public class ServicesValuesAddOrUpdateRequest
    {
        public int id { get; set; }
        public string value { get; set; }
        public int Serviceid { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile? file { get; set; }
    }
}
