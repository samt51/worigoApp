using System.Collections.Generic;
using Worigo.Entity.Concrete;

namespace Worigo.Core.Dtos.ListDto
{
   public class RoomTypeDto: BaseEntity
    {
        public int id { get; set; }
        public string typeName { get; set; }
    }
}
