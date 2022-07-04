using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class EmployeesManager : IEmployeesService
    {
        private readonly IEmployeesDal _employeesDal;
        public EmployeesManager(IEmployeesDal employeesDal)
        {
            _employeesDal = employeesDal;
        }
        public void Create(Employees entity)
        {
            _employeesDal.Create(entity);
        }

        public List<EmployeesListJoin> employeesListJoins(int hotelid)
        {
            return _employeesDal.employeesListJoins(hotelid);
        }

        public List<Employees> GetAll()
        {
            return _employeesDal.GetAll();
        }

        public Employees GetById(int id)
        {
            return  _employeesDal.GetById(id);
        }

        public void Update(Employees entity)
        {
            _employeesDal.Update(entity);
        }
    }
}
