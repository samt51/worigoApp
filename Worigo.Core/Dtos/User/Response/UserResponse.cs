namespace Worigo.Core.Dtos.User.Response
{
    public class UserResponse
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int roleid { get; set; }
        public string roleName { get; set; }
        public int companyid { get; set; }
        public string companyName { get; set; }
    }
}
