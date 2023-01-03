using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worigo.Core.Exceptions;

namespace Worigo.Core.Dtos.JoinClass.AuthorizationClassView
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
                    userId = Convert.ToInt32(decodedValue.Payload.Where(x => x.Key == "userId").FirstOrDefault().Value),
                    role = Convert.ToInt32(decodedValue.Payload.Where(x => x.Key == "role").FirstOrDefault().Value),
                    companyid = Convert.ToInt32(decodedValue.Payload.Where(x => x.Key == "companyid").FirstOrDefault().Value)
                };
                return mytoken;
            }
            else throw new AuthorizationException("Authorization Failed");
        }
    }
    public class TokenKeys
    {
        public int userId { get; set; }
        public string email { get; set; }
        public int role { get; set; }
        public int companyid { get; set; }
    }
}
