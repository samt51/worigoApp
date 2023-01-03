using Microsoft.AspNetCore.Http;

namespace Worigo.Core.Dtos.Departman.Request
{
    public class DepartmentAddOrUpdateRequest
    {
        public int Id { get; set; }
        public string DepartmanName { get; set; }
        public IFormFile? file { get; set; }
        public string ImageUrl { get; set; }
    }
}
