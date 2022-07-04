using System.Collections.Generic;
using Worigo.Entity.Concrete;

namespace Worigo.Core.Dtos.ListDto
{
    public class DepartmanDto: BaseEntity
    {
        public int Id { get; set; }
        public string DepartmanName { get; set; }
        public int Hotelid { get; set; }
        public bool isActive { get; set; }

    }
}
