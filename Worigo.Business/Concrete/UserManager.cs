using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Worigo.Business.Abstrack;
using Worigo.Business.Encryption;
using Worigo.Core.Dtos.JoinClass;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IConfiguration _configuration;
        public UserManager(IUserDal userDal, IConfiguration configuration)
        {
            _userDal = userDal;
            _configuration = configuration;
        }
        public void Create(User entity)
        {
            entity.password = CommodMethods.ConvertToEncryp(entity.password);
            _userDal.Create(entity);
        }

        public List<User> GetAll()
        {
            return _userDal.GetAll(x => x.isDeleted == false);
        }

        public List<UserAndUserRoleJoin> GetAllJoin()
        {
            return _userDal.GetAllJoin();
        }

        public User GetById(int id)
        {
            return _userDal.GetById(id);
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            password = CommodMethods.ConvertToEncryp(password);
            return _userDal.GetUserByEmailAndPassword(email, password);
        }

        public string ProduceToken(string id, string email, string role,string hotelid)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("TokenKey"));
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userId", id),
                    new Claim(ClaimTypes.Email, email),
                    new Claim("role", role),
                    new Claim("hotelid", hotelid)

                }),
                Expires = DateTime.UtcNow.AddYears(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);
            return token;
        }

        public void Update(User entity)
        {
            _userDal.Update(entity);
        }
    }
}
