namespace Worigo.Core.Dtos.User.Request
{
    public class UserRequest
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int roleid { get; set; }
        public int companyid { get; set; }
    }
}
