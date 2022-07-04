namespace Worigo.Core.Dtos.ListDto
{
    public class UserListDto
    {
        public int id { get; set; }
        public string email { get; set; }
        public int roleid { get; set; }
        public int? hotelid { get; set; }
        public bool isActive { get; set; }
    }
}
