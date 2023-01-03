using System.Collections.Generic;
using Worigo.Core.Dtos.EmployeeType.Response;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface IEmployeesTypeDal: IRepositoryDesignPattern<EmployeesType>
    {
        List<EmployeeTypeResponse> GetEmployeesTypeByDepartmanid(int departmanid);    
    }
}
