using System.Collections.Generic;
using System.Linq;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfUserRoleDal : EfRepositoryDal<UserRole, DataContext>, IUserRoleDal
    {
        public List<UserRole> ForHotelsListUserRole(bool systemadmin)
        {
            using (var db=new DataContext())
            {
                if (systemadmin == true)
                    return db.UserRole.ToList();
                return db.UserRole.Where(x=>x.id!=1).ToList();
            }
        }
    }
}
