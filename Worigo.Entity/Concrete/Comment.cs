using Worigo.Entity.Abstrack;
namespace Worigo.Entity.Concrete
{
    public class Comment:IBaseEntity
    {
        public int Id { get; set; }
        public int employeesid{ get; set; }
        public int Point { get; set; }
        public int hotelid { get; set; }
        public string Commentary { get; set; }
    }
}
