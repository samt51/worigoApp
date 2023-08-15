using Microsoft.AspNetCore.Http;

namespace Worigo.Core.Dtos.Services.Request
{
    public class ServicesAddOrUpdateRequest
    {
        public int id { get; set; }
        public string Name { get; set; }
        public IFormFile? file { get; set; }
        public string ImageUrl { get; set; }
    }
}
