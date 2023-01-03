using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class TasksOfEmployeesManager : ITasksOfEmployeesService
    {
        private readonly ITasksOfEmployeesDal _tasksOfEmployeesDal;
        public TasksOfEmployeesManager(ITasksOfEmployeesDal tasksOfEmployeesDal)
        {
            _tasksOfEmployeesDal = tasksOfEmployeesDal;
        }
    
        public TasksOfEmployees Create(TasksOfEmployees entity)
        {
          return  _tasksOfEmployeesDal.Create(entity);
        }

        public List<TasksOfEmployees> GetAll()
        {
            return _tasksOfEmployeesDal.GetAll();
        }

        public TasksOfEmployees GetById(int id)
        {
            return _tasksOfEmployeesDal.GetById(id);
        }

        public TasksOfEmployees Update(TasksOfEmployees entity)
        {
         return   _tasksOfEmployeesDal.Update(entity);
        }
    }
}
