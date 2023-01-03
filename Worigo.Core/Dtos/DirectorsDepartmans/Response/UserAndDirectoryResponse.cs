using System;
using System.Collections.Generic;
using Worigo.Core.Dtos.DirectorsDepartmans.Dto;

namespace Worigo.Core.Dtos.DirectorsDepartmans.Response
{
    public class UserAndDirectoryResponse
    {
        public int DirectoryEmployeeId { get; set; }
        public int employeeid { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string imageurl { get; set; }
        public string phonenumber { get; set; }
        public bool gender { get; set; }
        public string email { get; set; }
        public string password  { get; set; }
        public int hotelid { get; set; }
        public string HotelName { get; set; }
        public DateTime? StartDateOfWork { get; set; }
        public DateTime? ExitEntryDate { get; set; }
        public List<DirectoryDepartmentResponseAllDepartmentResponse> AllDepartment { get; set; }
    }
}
