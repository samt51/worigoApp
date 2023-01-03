using System.Collections.Generic;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface ITasksOfEmployeesService
    {
        List<TasksOfEmployees> GetAll();
        TasksOfEmployees GetById(int id);
        TasksOfEmployees Create(TasksOfEmployees entity);
        TasksOfEmployees Update(TasksOfEmployees entity);
    }
}
