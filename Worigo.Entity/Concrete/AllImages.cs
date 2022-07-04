using Worigo.Entity.Abstrack;
namespace Worigo.Entity.Concrete
{
    public class AllImages:IBaseEntity
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
    }
}
