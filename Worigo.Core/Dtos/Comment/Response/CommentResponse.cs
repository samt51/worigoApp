using System;

namespace Worigo.Core.Dtos.Comment.Response
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public int employeesid { get; set; }
        public string EmployeeNameAndSurname { get; set; }
        public string EmployeePositionName { get; set; }
        public int EmployeePoint { get; set; }
        public int speedPoint { get; set; }
        public int contentsPoint { get; set; }
        public double GeneralPoint { get; set; }
        public int hotelid { get; set; }
        public string Hotel { get; set; }
        public string Commentary { get; set; }
        public string Service { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
