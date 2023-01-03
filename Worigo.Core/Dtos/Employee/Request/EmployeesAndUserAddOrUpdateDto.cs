using Microsoft.AspNetCore.Http;
using System;

namespace Worigo.Core.Dtos.Employee.Request
{
    public class EmployeesAndUserAddOrUpdateDto
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public int? FloorNo { get; set; }
        public string phoneNumber { get; set; }
        public bool gender { get; set; }
        public DateTime? StartDateOfWork { get; set; }
        public DateTime? ExitEntryDate { get; set; }
        public int? employeestypeid { get; set; }

    }
    public class EmployeesAndUserListDto
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImageUrl { get; set; }
        public int? FloorNo { get; set; }
        public string phoneNumber { get; set; }
        public string hotel { get; set; }
        public string companiesname { get; set; }
        public bool gender { get; set; }
        public DateTime? StartDateOfWork { get; set; }
        public DateTime? ExitEntryDate { get; set; }
        public string employeestypename { get; set; }

    }
}
