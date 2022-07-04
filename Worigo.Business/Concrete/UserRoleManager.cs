using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class UserRoleManager : IUserRoleService
    {
        private readonly IUserRoleDal _userRoleDal;
        public UserRoleManager(IUserRoleDal userRoleDal)
        {
            _userRoleDal = userRoleDal;
        }
        public void Create(UserRole entity)
        {
            _userRoleDal.Create(entity);

        }

        public List<UserRole> GetAll()
        {
            return _userRoleDal.GetAll();
        }

        public UserRole GetById(int id)
        {
           return _userRoleDal.GetById(id);
        }

        public void Update(UserRole entity)
        {
            _userRoleDal.Update(entity);
        }
    }
}
