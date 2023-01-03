using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class Departman:IBaseEntity
    {
        public int Id { get; set; }
        public string DepartmanName { get; set; }
        public string ImageUrl { get; set; }

    }
}