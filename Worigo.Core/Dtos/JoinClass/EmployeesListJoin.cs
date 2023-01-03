using System;

namespace Worigo.Core.Dtos.JoinClass
{
    public class EmployeesListJoin
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImageUrl { get; set; }
        public int? FloorNo { get; set; }
        public string phoneNumber { get; set; }
        public bool gender { get; set; }
        public DateTime? StartDateOfWork { get; set; }
        public DateTime? ExitEntryDate { get; set; }
        public string user { get; set; }
        public string hotel { get; set; }
        public int departmanid { get; set; }
        public string jobs { get; set; }
        public bool isActive { get; set; }
        public DateTime? onlineTime { get; set; }


    }
}
