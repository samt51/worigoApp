using System.Collections.Generic;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface ICommentService 
    {
        List<CommentListJoin> commentListJoins(int hotelid);
        List<Comment> GetAll();
        Comment GetById(int id);
        void Create(Comment entity);
        void Update(Comment entity);
    }
}
