using Microsoft.AspNetCore.Http;
using System;

namespace Worigo.Core.Dtos.Employee.Request
{
    public class EmployeeRequest
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImageUrl { get; set; }
        public int? FloorNo { get; set; }
        public string phoneNumber { get; set; }
        public bool gender { get; set; }
        public IFormFile? file{ get; set; }
        public DateTime? StartDateOfWork { get; set; }
        public DateTime? ExitEntryDate { get; set; }
        public int userid { get; set; }
        public int? hotelid { get; set; }
        public int? employeestypeid { get; set; }
        public DateTime? onlineTime { get; set; }
        public bool OnlineOrOfflineNow { get; set; }
    }
}
