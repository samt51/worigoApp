using System.Collections.Generic;
using Worigo.Core.Dtos.JoinClass;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface IUserDal:IRepositoryDesignPattern<User>
    {
        User GetUserByEmailAndPassword(string email, string password);
        List<UserAndUserRoleJoin> GetAllJoin();
    }
}
