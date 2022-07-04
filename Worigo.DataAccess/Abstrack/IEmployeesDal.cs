using System.Collections.Generic;
using Worigo.Core.Dtos.JoinClass;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface IEmployeesDal: IRepositoryDesignPattern<Employees>
    {
        List<EmployeesListJoin> employeesListJoins(int hotelid);
    }
}
