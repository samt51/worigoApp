using System.Collections.Generic;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
   public interface IEmployeesService
    {
        List<EmployeesListJoin> employeesListJoins(int hotelid);
        List<Employees> GetAll();
        Employees GetById(int id);
        void Create(Employees entity);
        void Update(Employees entity);
    }
}
