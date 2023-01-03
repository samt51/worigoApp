using System;

namespace Worigo.Core.Dtos.JoinClass
{
    public class UserAndEmployeesJoin
    {
        public int id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string? FloorNumber { get; set; }
        public string phoneNumber { get; set; }
        public bool Gender { get; set; }
        public DateTime? StartDateOfWork { get; set; }
        public DateTime? ExitEntryDate { get; set; }
        public string HotelName { get; set; }
        public string Departman { get; set; }
        public string Company { get; set; }
        public bool isActive { get; set; }


    }
}
