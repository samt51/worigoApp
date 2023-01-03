using System.Collections.Generic;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface IEmployeeOfOrderService
    {
     
        List<EmployeeOfOrder> GetAll();
        EmployeeOfOrder GetById(int id);
        EmployeeOfOrder Create(EmployeeOfOrder entity);
        EmployeeOfOrder Update(EmployeeOfOrder entity);
    }
}
