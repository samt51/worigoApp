using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.JoinClass;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfCompaniesDal : EfRepositoryDal<Companies, DataContext>, ICompaniesDal
    {
      
    }
}
