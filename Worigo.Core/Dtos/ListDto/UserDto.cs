namespace Worigo.Core.Dtos.ListDto
{
    public class UserDto
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int roleid { get; set; }
        public int hotelid { get; set; }
        public bool isActive { get; set; }


    }
}
