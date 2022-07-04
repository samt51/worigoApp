using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Worigo.API.Model.UserViewModel
{
    public class AuthorizationCont
    {
        public static TokenKeys Authorization(string Token)
        {
            if (!string.IsNullOrEmpty(Token) && Token.StartsWith("Bearer ") == true)
            {
                var handler = new JwtSecurityTokenHandler();
                var decodedValue = handler.ReadJwtToken(Token.Replace("Bearer ", ""));
                TokenKeys mytoken = new TokenKeys
                {
                    email = decodedValue.Payload.Where(x => x.Key == "email").FirstOrDefault().Value.ToString(),
                    userId = ValueSecret.integer(decodedValue.Payload.Where(x => x.Key == "userId").FirstOrDefault().Value),
                    role = ValueSecret.integer(decodedValue.Payload.Where(x => x.Key == "role").FirstOrDefault().Value),
                    hotelid= ValueSecret.integer(decodedValue.Payload.Where(x => x.Key == "hotelid").FirstOrDefault().Value),
                };
                return mytoken;
            }
            else return new TokenKeys();
        }
    }
    public class TokenKeys
    {
        public int userId { get; set; }
        public string email { get; set; }
        public int role { get; set; }
        public int hotelid { get; set; }
    }
}
