﻿using Worigo.Entity.Abstrack;
namespace Worigo.Entity.Concrete
{
    public class Comment : IBaseEntity
    {
        public int Id { get; set; }
        public int speedPoint { get; set; }
        public int contentsPoint { get; set; }
        public int hotelid { get; set; }
        public string Commentary { get; set; }
        public int EmployeePoint { get; set; }
        public int OrderId { get; set; }
    }
}
