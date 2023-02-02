using System;

namespace Worigo.Core.Dtos.User.Dto
{
    public class ManagementUserResponse
    {
        public int Id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string imageurl { get; set; }
        public string phonenumber { get; set; }
        public bool gender { get; set; }
        public int companyId { get; set; }
        public string CompanyName { get; set; }
        public DateTime? StartDateOfWork { get; set; }
        public DateTime? ExitEntryDate { get; set; }
        public int? employeestypeid { get; set; }
        public string EmployeeTypeName { get; set; }
    }
}
