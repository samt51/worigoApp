using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class EmployeesType:IBaseEntity
    {
        public int id { get; set; }
        public string TypeName { get; set; }
        public int departmanid { get; set; }
    }
}
