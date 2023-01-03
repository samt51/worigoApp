using System.Collections.Generic;
using Worigo.Core.Dtos.JoinClass;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.Reports.HotelGeneralPuan;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Abstrack
{
    public interface ICommentService
    {
        CommentListJoin GetByIdJoin(int id);
        List<CommentListJoin> commentListJoins(int hotelid);
        List<Comment> GetAll();
        Comment GetById(int id);
        Comment Create(Comment entity);
        Comment Update(Comment entity);
        ResponseDto<HotelGeneralPointResponse> HotelGeneralPointByHotelId(int hotelid,TokenKeys keys);
    }
}
