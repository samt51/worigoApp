namespace Worigo.Core.Dtos.Comment.Request
{
    public class CommentAddOrUpdateRequest
    {
        public int Id { get; set; }
        public int employeesid { get; set; }
        public int Point { get; set; }
        public int speedPoint { get; set; }
        public int contentsPoint { get; set; }
        public int hotelid { get; set; }
        public string Commentary { get; set; }
    }
}
