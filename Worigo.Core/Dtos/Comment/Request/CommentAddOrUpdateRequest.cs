namespace Worigo.Core.Dtos.Comment.Request
{
    public class CommentAddOrUpdateRequest
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int speedPoint { get; set; }
        public int contentsPoint { get; set; }
        public int EmployeePoint { get; set; }
        public string Commentary { get; set; }
        public int hotelId { get; set; }
        public bool IsActive { get; set; }
    }
}
