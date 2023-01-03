namespace Worigo.Core.Dtos.JoinClass
{
    public class AddHotelAdminModelDto
    {
        public int AdminUserId { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string imageurl { get; set; }
        public string phonenumber { get; set; }
        public bool gender { get; set; }
        public string CompanyName { get; set; }
    }
}
