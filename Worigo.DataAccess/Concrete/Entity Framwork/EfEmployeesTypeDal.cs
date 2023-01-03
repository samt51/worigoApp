using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.EmployeeType.Response;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfEmployeesTypeDal : EfRepositoryDal<EmployeesType, DataContext>, IEmployeesTypeDal
    {
        public List<EmployeeTypeResponse> GetEmployeesTypeByDepartmanid(int departmanid)
        {
            using (var db=new DataContext())
            {
                var joinlist = from d1 in db.employeesType.Where(x => x.departmanid == departmanid && x.isDeleted == false)
                               join d2 in db.Departman on d1.departmanid equals d2.Id
                               select new EmployeeTypeResponse
                               {
                                   DepartmentName=d2.DepartmanName,
                                   id=d1.id,
                                   TypeName=d1.TypeName,
                                   departmanid=d1.departmanid
                               };
                return joinlist.ToList();
            }
        }
    }
}
