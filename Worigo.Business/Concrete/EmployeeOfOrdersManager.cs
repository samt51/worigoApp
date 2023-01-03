using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class EmployeeOfOrdersManager : IEmployeeOfOrderService
    {
        private readonly IEmployeeOfOrderDal _employeeOfOrderDal;
        public EmployeeOfOrdersManager(IEmployeeOfOrderDal employeeOfOrderDal)
        {
            _employeeOfOrderDal = employeeOfOrderDal;
        }

        public EmployeeOfOrder Create(EmployeeOfOrder entity)
        {
          return _employeeOfOrderDal.Create(entity);
        }

        public List<EmployeeOfOrder> GetAll()
        {
            return _employeeOfOrderDal.GetAll();
        }

        public EmployeeOfOrder GetById(int id)
        {
            return _employeeOfOrderDal.GetById(id);
        }

        public EmployeeOfOrder Update(EmployeeOfOrder entity)
        {
           return _employeeOfOrderDal.Update(entity);
        }
    }
}
