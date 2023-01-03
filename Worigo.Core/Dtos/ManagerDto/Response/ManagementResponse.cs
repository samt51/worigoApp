using System;
using System.Collections.Generic;
using Worigo.Core.Dtos.ManagerDto.Dto;

namespace Worigo.Core.Dtos.ManagerDto.Response
{
    public class ManagementResponse
    {
        public int ManagementId { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string imageurl { get; set; }
        public string phonenumber { get; set; }
        public string employeestypename { get; set; }
        public string companiesname { get; set; }
        public bool gender { get; set; }
        public DateTime? StartDateOfWork { get; set; }
        public DateTime? ExitEntryDate { get; set; }
        public List<ManagementResponseHotelResponse> HotelList{ get; set; }
    }
}
