using System.Collections.Generic;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface IUserRoleDal: IRepositoryDesignPattern<UserRole>
    {
        List<UserRole> ForHotelsListUserRole(bool systemadmin);
    }
}
