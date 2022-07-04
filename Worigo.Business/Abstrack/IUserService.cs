using System.Collections.Generic;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IUserService
    {
        List<UserAndUserRoleJoin> GetAllJoin();
        string ProduceToken(string id,string email,string role,string hotelid);
        User GetUserByEmailAndPassword(string email, string password);
        List<User> GetAll();
        User GetById(int id);
        void Create(User entity);
        void Update(User entity);
    }
}
