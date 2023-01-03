using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.DataAccess.Concrete.Entity_Framwork;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete
{
    public class EfEmployeeOfOrderDal: EfRepositoryDal<EmployeeOfOrder, DataContext>, IEmployeeOfOrderDal
    {
    }
}
