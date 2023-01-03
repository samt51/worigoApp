namespace Worigo.Core.Dtos.Reports.HotelGeneralPuan
{
    public class HotelGeneralPointResponse
    {
        public string HotelName { get; set; }
        public decimal HotelAveragePoint { get; set; }
        public decimal EmployeePoint { get; set; }
        public decimal SpeedPoint { get; set; }
        public decimal ServicePoint { get; set; }
        public int CommentCount { get; set; }
    }
}
