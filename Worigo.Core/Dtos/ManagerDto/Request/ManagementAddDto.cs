using System;
using System.Collections.Generic;

namespace Worigo.Core.Dtos.ManagerDto.Request
{
    public class ManagementAddDto
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string imageurl { get; set; }
        public string phonenumber { get; set; }
        public bool gender { get; set; }
        public int? FloorNo { get; set; }
        public DateTime? StartDateOfWork { get; set; }
        public DateTime? ExitEntryDate { get; set; }
    }

    public class HotelAndManagementAdd
    {
        public int hotelid { get; set; }
        public List<ManagementAddByHotelid> employeesList { get; set; }
    }
    public class ManagementAddByHotelid
    {
        public int employeesid { get; set; }
    }

}
