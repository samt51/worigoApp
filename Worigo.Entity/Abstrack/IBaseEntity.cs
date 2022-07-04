using System;

namespace Worigo.Entity.Abstrack
{
    public abstract class IBaseEntity
    {
        public bool isDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public bool isActive { get; set; }
    }
}
