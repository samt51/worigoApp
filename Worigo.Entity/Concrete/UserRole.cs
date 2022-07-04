using System.Collections.Generic;
using Worigo.Entity.Abstrack;

namespace Worigo.Entity.Concrete
{
    public class UserRole:IBaseEntity
    {
        public int id { get; set; }
        public string RoleName { get; set; }

    }
}
