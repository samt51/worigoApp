using Worigo.Entity.Concrete;

namespace Worigo.Core.Dtos.ListDto
{
    public class ServicesDto: BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Hotelid { get; set; }
        public bool isActive { get; set; }
    }
}
