using System.Collections.Generic;
using Worigo.Core.Dtos.JoinClass;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface ICommentDal:IRepositoryDesignPattern<Comment>
    {
        List<CommentListJoin> commentListJoins(int hotelid);
    }
}
