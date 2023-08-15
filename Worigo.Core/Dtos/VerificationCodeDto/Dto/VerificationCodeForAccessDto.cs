using System;

namespace Worigo.Core.Dtos.VerificationCodeDto.Dto
{
    public class VerificationCodeForAccessDto
    {
        public int id { get; set; }
        public int roomid { get; set; }
        public string RoomNo { get; set; }
        public int hotelid { get; set; }
        public string HotelName { get; set; }
        public string Phone { get; set; }
        public int CompanyId { get; set; }
        public int? CustomerId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Code { get; set; }
        public bool IsFull { get; set; }
        public string Token { get; set; }
    }
}
