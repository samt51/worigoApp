using System;

namespace Worigo.Core.Dtos.ListDto
{
    public class BaseEntity
    {
        public bool isDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
