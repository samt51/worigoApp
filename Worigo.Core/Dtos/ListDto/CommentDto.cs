using Worigo.Entity.Concrete;

namespace Worigo.Core.Dtos.ListDto
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int employeesid { get; set; }
        public int Point { get; set; }
        public string Commentary { get; set; }
        public bool isActive { get; set; }
    }
}
