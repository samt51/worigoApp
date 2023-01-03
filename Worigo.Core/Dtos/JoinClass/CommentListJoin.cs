using System;

namespace Worigo.Core.Dtos.JoinClass
{
    public class CommentListJoin
    {
        public int Id { get; set; }
        public string EmployeeNameAndSurname { get; set; }
        public int EmployeePoint { get; set; }
        public int SpeedPoint { get; set; }
        public int ContentsPoint { get; set; }
        public int GeneralPoint { get; set; }
        public string Hotel { get; set; }
        public string Commentary { get; set; }
        public string EmployeesType { get; set; }
        public string Service { get; set; }
        public string ServiceContent { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
