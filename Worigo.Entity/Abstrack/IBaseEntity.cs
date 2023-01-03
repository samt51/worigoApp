using System;

namespace Worigo.Entity.Abstrack
{
    public abstract class IBaseEntity
    {
        public bool isDeleted { get; set; }=false;
        public DateTime CreatedDate { get; set; }=DateTime.Now;
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public bool isActive { get; set; } = true;
    }
}
