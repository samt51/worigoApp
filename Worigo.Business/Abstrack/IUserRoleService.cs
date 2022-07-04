using System.Collections.Generic;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IUserRoleService
    {
        List<UserRole> GetAll();
        UserRole GetById(int id);
        void Create(UserRole entity);
        void Update(UserRole entity);
    }
}
