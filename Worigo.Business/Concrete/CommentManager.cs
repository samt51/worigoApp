using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.JoinClass;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class CommentManager : ICommentService
    {
        private readonly ICommentDal _commentDal;
        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public List<CommentListJoin> commentListJoins(int hotelid)
        {
            return _commentDal.commentListJoins(hotelid);
        }

        public void Create(Comment entity)
        {
            _commentDal.Create(entity);
        }

        public List<Comment> GetAll()
        {
            return _commentDal.GetAll(x => x.isDeleted == false);
        }

        public Comment GetById(int id)
        {
            return _commentDal.GetById(id);
        }

        public void Update(Comment entity)
        {
            _commentDal.Update(entity);
        }
    }
}
