using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.JoinClass;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfCommentDal : EfRepositoryDal<Comment, DataContext>, ICommentDal
    {
        public List<CommentListJoin> commentListJoins(int hotelid)
        {
            using (var db=new DataContext())
            {
                var joinlist = from d1 in db.Comment.Where(x => x.hotelid == hotelid && x.isDeleted == false)
                               join d2 in db.Hotel on d1.hotelid equals d2.id
                               join d3 in db.Employees on d1.employeesid equals d3.id
                               select new CommentListJoin
                               {
                                   Commentary=d1.Commentary,
                                   employees=d3.Name+" " +d3.Surname,
                                   hotel=d2.HotelName,
                                   Id=d1.Id
                               };
                return joinlist.ToList();
            };
        }
    }
}
