using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json.Serialization;

namespace Worigo.Core.Dtos.DirectorsDepartmans.Request
{
    public class UserAndDirectoryDepartmentAddOrUpdateRequest
    {
        public int directoryEmployeeId { get; set; }
        public int hotelid { get; set; }
        public int directoryid { get; set; }
        public int departmanid { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        [JsonIgnore]
        public int roleid { get; set; } = 5;
        public int companyid { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime StartDateOfWork { get; set; }
        public DateTime? ExitEntryDate { get; set; }
        public IFormFile file{ get; set; }
        public bool gender { get; set; }
        public string phoneNumber { get; set; }
        public int userid { get; set; }
    }
}
